using System;
using System.Collections.Generic;

namespace HealthCare.Repository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(Func<T, bool> func, T entity);
        void Remove(Func<T, bool> func);
        IEnumerable<T> GetAll();
    }
}

