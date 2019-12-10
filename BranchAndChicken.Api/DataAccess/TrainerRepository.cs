using BranchAndChicken.Api.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace BranchAndChicken.Api.DataAccess
{
    public class TrainerRepository
    {
        string _connectionString;

        public TrainerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("ConnectionString");
        }

        public List<Trainer> GetAll()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var trainers = db.Query<Trainer>("Select Id,Name,YearsOfExperience,Specialty From Trainer");

                foreach (var trainer in trainers)
                {
                    var chickenRepo = new ChickenRepository();
                    var chickensForTrainer = chickenRepo.GetChickensForTrainer(trainer.Id);

                    trainer.Coop.AddRange(chickensForTrainer);
                }

                return trainers.AsList();
            }
        }

        public Trainer Get(string name)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"select *
                            from Trainer
                            where Trainer.Name = @trainerName";

                var parameters = new {trainerName = name};

                var trainer = db.QueryFirst<Trainer>(sql, parameters);

                return trainer;
            }
        }

        public bool Remove(string name)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"delete 
                            from trainer 
                            where [name] = @name";

                return db.Execute(sql, new {name}) == 1;
            }
        }

        public ActionResult<Trainer> GetSpecialty(string specialty)
        {
            throw new NotImplementedException();
        }

        public Trainer Update(Trainer updatedTrainer, int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE [Trainer]
                                SET [Name] = @name
                                    ,[YearsOfExperience] = @yearsOfExperience
                                    ,[Specialty] = @specialty
                            output inserted.*
                                WHERE id = @id";

                //var parameters = new
                //{
                //    Id = id,
                //    Name = updatedTrainer.Name,
                //    YearsOfExperience = updatedTrainer.YearsOfExperience,
                //    Specialty = updatedTrainer.Specialty
                //};

                updatedTrainer.Id = id;

                var trainer = db.QueryFirst<Trainer>(sql, updatedTrainer);
                
                return trainer;
            }
        }

        public Trainer Add(Trainer newTrainer)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO [Trainer]
                                           ([Name]
                                           ,[YearsOfExperience]
                                           ,[Specialty])
	                                 output inserted.*
                                     VALUES
                                           (@name
                                           ,@yearsOfExperience
                                           ,@specialty)";

                return db.QueryFirst<Trainer>(sql, newTrainer);
            }
        }
    }
}
