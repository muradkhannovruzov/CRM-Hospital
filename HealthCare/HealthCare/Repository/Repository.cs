using HealthCare.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HealthCare.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        readonly IFileService fileService;
        public string Path { get; set; } = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent
            (Directory.GetParent(Directory.GetParent("aasas").ToString()).ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + typeof(T).Name + ".json";
        //"C:\\Users\\zabil\\Documents\\GitHub\\FSDE_2912_az_WPF_Exam_Team1\\MainDB\\Patient.json";

        public Repository(IFileService fileService)
        {
            this.fileService = fileService;
        }
        public void Add(T entity)
        {

            var data = GetAll().ToList();
          

            data.Add(entity);

            string path = Path;

            fileService.JsonFileWrite(path, data);
        }
        public void Remove(Func<T, bool> func)
        {
            var data = GetAll().ToList();

            data.Remove(data.FirstOrDefault(func));

            string path = Path;

            fileService.JsonFileWrite(path, data);
        }
        public void Update(Func<T, bool> func, T entity)
        {
            var data = GetAll().ToList();

            data.Remove(data.FirstOrDefault(func));

            data.Add(entity);

            string path = Path;

            fileService.JsonFileWrite(path, data);
        }
        public IEnumerable<T> GetAll()
        {
            string path = Path;

            if (!File.Exists(path))
            {
                return new List<T>();
            }
            var list= JsonConvert.DeserializeObject<IEnumerable<T>>(File.ReadAllText(path));            
            return list;
        }
    }
}
