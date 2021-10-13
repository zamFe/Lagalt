using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Models.DTOs.Profession
{
    public class ProfessionCreateDTO
    {
        public string Name { get; set; }
        public int[] Projects { get; set; }
    }
}
