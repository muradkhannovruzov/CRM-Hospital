
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Service
{
    public interface IFileService
    {
        void JsonFileWrite(string path, object content);
        T JsonFileRead<T>(string path);
    }
}