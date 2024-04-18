using Eagles_Website.Models;
using System.Linq.Expressions;

namespace Eagles_Website.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeprop = null);
        T Get(Expression<Func<T, bool>> filter, string? includeprop = null);
        void add(T entity);
        void update(T entity);
        void save();
        void remove(T entity);
        void removeRange(IEnumerable<T> entities);
        IEnumerable<T> GetList(Expression<Func<T, bool>> filter, string? includeprop = null);

    }
}
