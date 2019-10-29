using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BranchAndChicken.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace BranchAndChicken.Api.DataAccess
{
    public class TrainerRepository
    {
        //static List<Trainer> _trainers = new List<Trainer>
        //{
        //    new Trainer
        //    {
        //        Name = "Nathan",
        //        Specialty = Specialty.TaeCluckDoe,
        //        YearsOfExperience = 0
        //    },
        //    new Trainer
        //    {
        //        Name = "Martin",
        //        Specialty = Specialty.Chudo,
        //        YearsOfExperience = 12
        //    },
        //    new Trainer
        //    {
        //        Name = "Adam",
        //        Specialty = Specialty.ChravBacaw,
        //        YearsOfExperience = 3
        //    }
        //};

        string _connectionString = "Server=localhost;Database=BranchAndChicken;Trusted_Connection=True;";

        public List<Trainer> GetAll()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"Select * 
                                From Trainer";

            var dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                //explicit cast
                var id = (int)dataReader["Id"];
                //implicit cast
                var name = dataReader["name"] as string;
                //convert to
                var yearsOfExperience = Convert.ToInt32(dataReader["YearsOfExperience"]);
                //try parse
                Enum.TryParse<Specialty>(dataReader["speciality"].ToString(), out var speciality);

                var trainer = new Trainer
                {
                    Specialty = speciality,
                    Id = id,
                    Name = name,
                    YearsOfExperience = yearsOfExperience
                };



            }


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
