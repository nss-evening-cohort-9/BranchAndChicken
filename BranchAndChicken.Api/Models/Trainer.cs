using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BranchAndChicken.Api.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearsOfExperience { get; set; }
        public Specialty Specialty { get; set; }
        public List<Chicken> Coop { get; set; }
    }

    public enum Specialty
    {
        Chudo,
        Chousting,
        TaeCluckDoe,
        ChravBacaw
    }
}
