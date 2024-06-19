using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GYMProject.Models; // Import your entity model namespace

namespace GYMProject.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            // Check if there is any entity in the DbSet<T> with the given id
            // This assumes the entity type T has an 'Id' property
            var entity = _dbSet.Find(id);
            return entity != null;
        }
    }
}
