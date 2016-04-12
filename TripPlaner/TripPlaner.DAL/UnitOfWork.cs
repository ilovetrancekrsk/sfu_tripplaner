using System.Threading.Tasks;

namespace TripPlaner.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TripPlanerDbContext _dbContext = new TripPlanerDbContext();

        public TripPlanerDbContext DbContext
        {
            get { return _dbContext; }
        }

        public void SaveChange()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
