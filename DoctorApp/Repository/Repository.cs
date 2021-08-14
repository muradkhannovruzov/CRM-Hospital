using DoctorApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace DoctorApp.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        readonly IFileService fileService;
        string path  = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent("aasas").ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + typeof(T).Name + ".json";

        public Repository(IFileService fileService)
        {

            this.fileService = fileService;
        }
        public void Add(T entity)
        {
            //var data = GetAll().ToList();
            List<T> data = new List<T>();
            try
            {
                data = GetAll().ToList();

            }
            catch (Exception)
            {


            }
            if (data.Count == 0)data = new List<T>();

            data.Add(entity);


            fileService.JsonFileWrite(path, data);
        }
        public void Remove(Func<T, bool> func)
        {
            var data = GetAll().ToList();

            data.Remove(data.FirstOrDefault(func));

            fileService.JsonFileWrite(path, data);
        }
        public void Update(Func<T, bool> func, T entity)
        {
            var data = GetAll().ToList();

            data.Remove(data.FirstOrDefault(func));

            data.Add(entity);

            fileService.JsonFileWrite(path, data);
        }
        public IEnumerable<T> GetAll()
        {
            return JsonConvert.DeserializeObject<IEnumerable<T>>(File.ReadAllText(path));
        }
    }
}
