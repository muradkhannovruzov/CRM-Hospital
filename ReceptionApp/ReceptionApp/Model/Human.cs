using System;
using System.Collections.Generic;
using System.Text;

namespace ReceptionApp.Model
{
    public class Human
    {
        public string Username { get; set; }
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Number { get; set; }
        public string Mail { get; set; }
        public Human()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
