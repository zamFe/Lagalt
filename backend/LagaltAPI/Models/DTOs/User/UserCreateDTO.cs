using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Models.DTOs.User
{
    public class UserCreateDTO
    {
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Portfolio { get; set; }
        public int[] Skills { get; set; }
        public int[] Projects { get; set; }
        public int[] Messages { get; set; }
    }
}
