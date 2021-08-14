using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorApp.Models
{
    public class Medicine
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public bool IsEnable { get; set; }
    }
}
