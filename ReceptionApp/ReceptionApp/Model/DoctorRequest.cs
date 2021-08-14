using System;
using System.Collections.Generic;
using System.Text;

namespace ReceptionApp.Model
{
    public class DoctorRequest
    {
        public string Id { get; set; }
        public ProssedurCategories Category { get; set; }
        public string DocName { get; set; }
        public DateTime RequestTime { get; set; }
        public DoctorRequest()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
