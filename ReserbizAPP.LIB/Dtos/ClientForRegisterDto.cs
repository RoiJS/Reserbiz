using System;

namespace ReserbizAPP.LIB.Dtos
{
    public class ClientForRegisterDto
    {
        public string Name { get; set; }
        public string DbName { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public DateTime DateJoined { get; set; }
    }
}