using LibraryStoreApp.Utility;

namespace LibraryStoreApp.Models
{
    public class BookTypeRepository : Repository<BookType> ,IBookTypeRepository
    {
        private AppDBContext _appDbContext;
        public BookTypeRepository(AppDBContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(BookType bookType)
        {
            _appDbContext.Update(bookType);
        }
    }
}
