using System;
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Models
{
    public class Client : Entity
    {
        public string Name { get; set; }
        public string DBName { get; set; }
        public string DBHashName { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public ClientTypeEnum Type { get; set; } = ClientTypeEnum.Regular;
        public DateTime DateJoined { get; set; }
    }
}