using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    public enum TypeOfRoom
    {
        Bufet,
        Satamtoloji,
        Opersiya,
        Reanimasiya
        //ve s. 

    }
   public  class Room
    {
        public int Number { get; set; }
        public TypeOfRoom RoomType { get; set; }
    }
}
