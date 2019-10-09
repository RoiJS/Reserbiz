using System;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.models;

namespace ReserbizAPP.LIB.Models
{
    public class Account : Person
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }

    }
}