using LibraryStoreApp.Utility;

namespace LibraryStoreApp.Models
{
    public class LeasingRepository : Repository<Leasing> , ILeasingRepository    {
        private AppDBContext _dbContext;

        public LeasingRepository(AppDBContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public void Update(Leasing leasing) 
        {
            _dbContext.Update(leasing); 
        }

        public void Save() 
        {
            _dbContext.SaveChanges();
        }

    }
}
