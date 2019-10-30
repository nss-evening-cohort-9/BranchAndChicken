using System;
using System.Collections.Generic;
using System.Data;
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
                    trainers.Add(GetTrainerFromDataReader(dataReader));
                }
                return trainers;
            }
        }

        public Trainer Get(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select *
                                     from Trainer
                                     where Trainer.Name = @trainerName";

                cmd.Parameters.AddWithValue("trainerName", name);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return GetTrainerFromDataReader(reader);
                }
            }

            return null;
        }

        public bool Remove(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = @"delete 
                                    from trainer 
                                    where [name] = @name";

                cmd.Parameters.AddWithValue("name", name);

                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public ActionResult<Trainer> GetSpecialty(string specialty)
        {
            throw new NotImplementedException();
        }

        public Trainer Update(Trainer updatedTrainer, int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = @"
                            UPDATE [Trainer]
                                SET [Name] = @name
                                    ,[YearsOfExperience] = @yearsOfExperience
                                    ,[Specialty] = @specialty
                            output inserted.*
                                WHERE id = @id";

                cmd.Parameters.AddWithValue("name", updatedTrainer.Name);
                cmd.Parameters.AddWithValue("yearsOfExperience", updatedTrainer.YearsOfExperience);
                cmd.Parameters.AddWithValue("specialty", updatedTrainer.Specialty);
                cmd.Parameters.AddWithValue("id", id);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return GetTrainerFromDataReader(reader);
                }

                return null;
            }
        }

        public Trainer Add(Trainer newTrainer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO [Trainer]
                                           ([Name]
                                           ,[YearsOfExperience]
                                           ,[Specialty])
	                                 output inserted.*
                                     VALUES
                                           (@name
                                           ,@yearsOfExperience
                                           ,@specialty)";

                cmd.Parameters.AddWithValue("name", newTrainer.Name);
                cmd.Parameters.AddWithValue("yearsOfExperience", newTrainer.YearsOfExperience);
                cmd.Parameters.AddWithValue("specialty", newTrainer.Specialty);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return GetTrainerFromDataReader(reader);
                }
            }

            return null;
        }


        Trainer GetTrainerFromDataReader(SqlDataReader reader)
        {
            //explicit cast
            var id = (int) reader["Id"];
            //implicit cast
            var name = reader["name"] as string;
            //convert to
            var yearsOfExperience = Convert.ToInt32(reader["YearsOfExperience"]);
            //try parse
            if (Enum.TryParse<Specialty>(reader["specialty"].ToString(), out var specialty))
            {
                //do something
            }

            var trainer = new Trainer
            {
                Specialty = specialty,
                Id = id,
                Name = name,
                YearsOfExperience = yearsOfExperience
            };

            return trainer;
        }

    }
}
