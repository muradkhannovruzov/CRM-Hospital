using Newtonsoft.Json;
using System.IO;

namespace HealthCare.Service
{
    public class FileService : IFileService
    {
        public T JsonFileRead<T>(string path)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void JsonFileWrite(string path, object content)
        {
            string json = JsonConvert.SerializeObject(content);
            File.WriteAllText(path, json);
        }
    }
}