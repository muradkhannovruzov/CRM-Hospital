using System;
using System.Collections.Generic;
using System.Text;

namespace ReceptionApp.Model
{
    public class Medicine
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public decimal Dose { get; set; }
        public bool IsEnable { get; set; }
        public int Count { get; set; }
    }
}
