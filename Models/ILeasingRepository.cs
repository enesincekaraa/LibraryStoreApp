namespace LibraryStoreApp.Models
{
    public interface ILeasingRepository : IRepository<Leasing>
    {
        void Save();
        void Update(Leasing leasing); 
    }
}
