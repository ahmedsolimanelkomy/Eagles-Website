using Eagles_Website.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using Eagles_Website.Models;

namespace Eagles_Website.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Context context;
        internal DbSet<T> dbSet;
        public Repository(Context _context)
        {
            context = _context;
            dbSet = context.Set<T>();
        }
        public void add(T entity)
        {
            dbSet.Add(entity);
           
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeprop = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeprop))
            {
                foreach (var inc in includeprop.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(inc);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeprop = null)
        {
            IQueryable<T> query = dbSet;

            if (!string.IsNullOrEmpty(includeprop))
            {
                foreach (var inc in includeprop.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(inc);
                }
            }
            return query.ToList();
        }

        public IEnumerable<T> GetList(Expression<Func<T, bool>> filter, string? includeprop = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeprop))
            {
                foreach (var inc in includeprop.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(inc);
                }
            }
            return query.ToList();
        }

        public void remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void removeRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public void save()
        {
            context.SaveChanges();
        }

        public void update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
