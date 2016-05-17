using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TripPlaner.DAL.Entities;

namespace TripPlaner
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //var json = config.Formatters.JsonFormatter;
            //json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            //json.SerializerSettings.Formatting = Formatting.None;
            //json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Создание модель-билдера для роутинга
            ODataModelBuilder builder = new ODataConventionModelBuilder();

            // добавление моделей в билдер
            builder.EntitySet<Placemark>("Placemarks")
                   .EntityType.HasKey(_ => _.Id);

            //регистрация роута для одата
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "odata",
                model: builder.GetEdmModel());

            // регистрация Autofac для OData
            config.DependencyResolver = AppBuilderExtensions
                .CreateAutofacWebApiDependencyResolver(null);
        }
    }
}
