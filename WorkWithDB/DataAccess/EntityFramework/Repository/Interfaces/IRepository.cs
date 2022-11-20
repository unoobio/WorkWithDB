using System.Linq.Expressions;

namespace WorkWithDB.DataAccess.EntityFramework.Repository.Interfaces
{
    public interface IRepository<T>
       where T : class
    {
        void Add(T entity);

        void Save();

        T Find(int id);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            bool isTracking = true
            );
    }
}
