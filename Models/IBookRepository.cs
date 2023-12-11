namespace LibraryStoreApp.Models
{
    public interface IBookRepository : IRepository<Book>
    {
        void Save();
        void Update(Book book); 
    }
}
