using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BranchAndChicken.Api.Models
{
    public class Chicken
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }
    }

    public enum Color
    {
        Black = 1,
        Brown,
        Blue,
        Pink
    }
}
