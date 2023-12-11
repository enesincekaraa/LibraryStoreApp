using LibraryStoreApp.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryStoreApp.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDBContext _appDBContext;
        internal DbSet<T> dbSet;

        public Repository(AppDBContext appDBContext) 
        {
            _appDBContext = appDBContext;
            this.dbSet = _appDBContext.Set<T>();
            _appDBContext.Books.Include(k => k.BookType);
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public T Get(Expression<Func<T, bool>> filtre, string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            query=query.Where(filtre);

            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProps=null)
        {
            IQueryable<T> query = dbSet;

            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query.ToList();
        }
    }
}
