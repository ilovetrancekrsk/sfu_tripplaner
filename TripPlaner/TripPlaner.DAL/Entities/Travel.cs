using System;
using System.Collections.Generic;

namespace TripPlaner.DAL.Entities
{
    /// <summary>
    /// Путешествие
    /// </summary>
    public class Travel : Entity
    {
        /// <summary>
        /// Название путешествия
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Начало путешествия
        /// </summary>
        public DateTime StartTravel { get; set; }
        /// <summary>
        /// Конец путешествия
        /// </summary>
        public DateTime EndTravel { get; set; }
        /// <summary>
        /// Места посещения
        /// </summary>
        public virtual ICollection<Placemark> Placemarks { get; set; }
    }
}
