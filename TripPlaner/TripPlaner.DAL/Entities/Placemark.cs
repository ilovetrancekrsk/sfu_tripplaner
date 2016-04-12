using System;

namespace TripPlaner.DAL.Entities
{
    /// <summary>
    /// Место посещения по маршруту
    /// </summary>
    public class Placemark : Entity
    {
        /// <summary>
        /// Название места для посещения
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание/заметка
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Широта
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// Долгота
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// Тип точки посещения
        /// </summary>
        public PlacemarkTypes Type { get; set; }
        /// <summary>
        /// Дата и время прибытия
        /// </summary>
        public DateTime Arrival { get; set; }
        /// <summary>
        /// Дата и время отбытия
        /// </summary>
        public DateTime Departure { get; set; }
        /// <summary>
        /// Статус посещения
        /// </summary>
        public bool IsVisited { get; set; }
    }
}
