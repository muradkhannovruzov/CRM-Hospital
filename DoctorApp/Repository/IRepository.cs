using System;
using System.Collections.Generic;

namespace DoctorApp.Repository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(Func<T, bool> func, T entity);
        void Remove(Func<T, bool> func);
        IEnumerable<T> GetAll();
    }
}

