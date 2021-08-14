using DoctorApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorApp.Services
{
    interface IAnalysisServices
    {
        IEnumerable<Analysis> FindAnalyses(IEnumerable<string> AnalysisID);
    }
}
