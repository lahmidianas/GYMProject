using System.Collections.Generic;

namespace GYMProject.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void SaveChanges(); // Ensure this method is defined
        bool Exists(int id);
    }
}
