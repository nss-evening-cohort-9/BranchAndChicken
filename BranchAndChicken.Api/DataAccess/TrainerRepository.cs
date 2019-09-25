using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BranchAndChicken.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BranchAndChicken.Api.DataAccess
{
    public class TrainerRepository
    {
        static List<Trainer> _trainers = new List<Trainer>
        {
            new Trainer
            {
                Name = "Nathan",
                Specialty = Specialty.TaeCluckDoe,
                YearsOfExperience = 0
            },
            new Trainer
            {
                Name = "Martin",
                Specialty = Specialty.Chudo,
                YearsOfExperience = 12
            },
            new Trainer
            {
                Name = "Adam",
                Specialty = Specialty.ChravBacaw,
                YearsOfExperience = 3
            }
        };

        public List<Trainer> GetAll()
        {
            return _trainers;
        }

        public Trainer Get(string name)
        {
            var trainer = _trainers.FirstOrDefault(t => t.Name == name);
            return trainer;
        }

        public void Remove(string name)
        {
            var trainer = Get(name);

            _trainers.Remove(trainer);
        }
    }
}
