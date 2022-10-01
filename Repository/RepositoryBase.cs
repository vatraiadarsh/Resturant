using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RepositoryContext _context;
        internal DbSet<T> DbSet;
        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public void Create(T entity) => DbSet.Add(entity);

        public void Delete(T entity) => DbSet.Remove(entity);

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ? DbSet.AsNoTracking() : DbSet;

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            DbSet.Where(expression).AsNoTracking() ?? DbSet.Where(expression);

        public void Update(T entity) => DbSet.Update(entity);

    }
}