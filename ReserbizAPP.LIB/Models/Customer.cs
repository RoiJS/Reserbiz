using System;

namespace ReserbizAPP.LIB.Models
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime DateEnded { get; set; }
    }
}