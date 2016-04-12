using System.ComponentModel.DataAnnotations;

namespace TripPlaner.DAL.Entities
{
    public abstract class Entity : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
