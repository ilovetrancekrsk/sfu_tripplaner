using System.Threading.Tasks;

namespace TripPlaner.DAL
{
    public interface IUnitOfWork
    {
        TripPlanerDbContext DbContext { get; } 

        void SaveChange();

        Task SaveChangeAsync();
    }
}
