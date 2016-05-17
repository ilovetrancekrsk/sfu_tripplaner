var AjaxODataHelper = function (odataUrl) {
    var $scope = {};
    $scope.url = odataUrl;

    $scope.get = function(key) {
        if (key) {
            return $.ajax($scope.url + '(' + key + ')');
        } else {
            return $.ajax($scope.url);
        }
    };

    $scope.post = function(data) {
        return $.post(data);
    };

    $scope.put = function (key, data) {
        if (key) {
            return $.ajax({ url: $scope.url, type: 'PUT' }, { key: key, data: data });
        } else {
            console.error('key is required');
        }
        return undefined;
    };

    $scope.patch = function (key, data) {
        if (key) {
            return $.ajax({ url: $scope.url, type: 'PATCH' }, { key: key, data: data });
        } else {
            console.error('key is required');
        }
        return undefined;
    };

    $scope.delete = function() {
        if (key) {
            return $.ajax({ url: $scope.url, type: 'DELETE' }, { key: key });
        } else {
            console.error('key is required');
        }
        return undefined;
    };

    return $scope;
};

var Application = function () {

    var $scope = {};

    $scope.map = L.map('map');

    $scope.placemarkPanel = ko.observable(false);
    $scope.activePlacemark = ko.observable({
        latitude: 0,
        longitude: 0
    });

    $scope.placemarkService = new AjaxODataHelper('/odata/Placemarks');

    $scope.run = function() {
        $scope.initMap();
    };

    $scope.initMap = function() {

        $scope.map.setView([51.505, -0.09], 13);

        L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
            attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
        }).addTo($scope.map);

        //var control = L.Routing.control({
        //    waypoints: [
        //        L.latLng(57.74, 11.94),
        //        L.latLng(57.6792, 11.949)
        //    ],
        //    geocoder: L.Control.Geocoder.nominatim(),
        //    routeWhileDragging: true,
        //    reverseWaypoints: true,
        //    showAlternatives: true,
        //    altLineOptions: {
        //        styles: [
        //            { color: 'black', opacity: 0.15, weight: 9 },
        //            { color: 'white', opacity: 0.8, weight: 6 },
        //            { color: 'blue', opacity: 0.5, weight: 2 }
        //        ]
        //    }
        //}).addTo($scope.map);

        //L.Routing.errorControl(control).addTo($scope.map);

        function createButton(label, container) {
            var btn = L.DomUtil.create('button', '', container);
            btn.setAttribute('type', 'button');
            btn.innerHTML = label;
            return btn;
        }

        $scope.map.on('click', function(e) {
            var container = L.DomUtil.create('div'),
                //startBtn = createButton('Start from this location', container),
                //destBtn = createButton('Go to this location', container),
                createBtn = createButton('create', container);


            //L.DomEvent.on(startBtn, 'click', function() {
            //    control.spliceWaypoints(0, 1, e.latlng);
            //    $scope.map.closePopup();
            //});

            //L.DomEvent.on(destBtn, 'click', function() {
            //    control.spliceWaypoints(control.getWaypoints().length - 1, 1, e.latlng);
            //    $scope.map.closePopup();
            //});


            L.DomEvent.on(createBtn, 'click', function () {
                $scope.placemarkPanel(true);
                $scope.activePlacemark({
                    latitude: e.latlng.lat,
                    longitude: e.latlng.lng
                });
            });

            L.popup()
                .setContent(container)
                .setLatLng(e.latlng)
                .openOn($scope.map);
        });
    };

    return $scope;
};