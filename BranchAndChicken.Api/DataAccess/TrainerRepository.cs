using System;
using System.Collections.Generic;
using System.Linq;
using BranchAndChicken.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace BranchAndChicken.Api.DataAccess
{
    public class TrainerRepository
    {
        string _connectionString = "Server=localhost;Database=BranchAndChicken;Trusted_Connection=True;";

        public List<Trainer> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = @"Select * 
                                From Trainer";

                var dataReader = cmd.ExecuteReader();

                var trainers = new List<Trainer>();

                while (dataReader.Read())
                {
                    //explicit cast
                    var id = (int) dataReader["Id"];
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

                    trainers.Add(trainer);
                }
                return trainers;
            }
        }

        public Trainer Get(string name)
        {
            //var trainer = _trainers.First(t => t.Name == name);
            //return trainer;
            return new Trainer();
        }

        public void Remove(string name)
        {
            //var trainer = Get(name);

            //_trainers.Remove(trainer);

        }

        public ActionResult<Trainer> GetSpecialty(string specialty)
        {
            throw new NotImplementedException();
        }

        public Trainer Update(Trainer updatedTrainer, int id)
        {
            //var trainerToUpdate = _trainers.First(trainer => trainer.Id == id);
            //trainerToUpdate.Name = updatedTrainer.Name;
            //trainerToUpdate.YearsOfExperience = updatedTrainer.YearsOfExperience;
            //trainerToUpdate.Specialty = updatedTrainer.Specialty;
            //return trainerToUpdate;
            return updatedTrainer;
        }

        public Trainer Add(Trainer newTrainer)
        {
            //_trainers.Add(newTrainer);
            return newTrainer;
        }
    }
}
