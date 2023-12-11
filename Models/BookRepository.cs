using LibraryStoreApp.Utility;

namespace LibraryStoreApp.Models
{
    public class BookRepository : Repository<Book> , IBookRepository
    {
        private AppDBContext _appDbContext;

        public BookRepository(AppDBContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(Book book)
        {
            _appDbContext.Update(book);
        }
    }
}
