using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BranchAndChicken.Api.Commands;
using BranchAndChicken.Api.Models;
using Dapper;

namespace BranchAndChicken.Api.DataAccess
{
    public class ChickenRepository
    {
        string _connectionString = "Server=localhost;Database=BranchAndChicken;Trusted_Connection=True;";
        public IEnumerable<Chicken> GetAll()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"select *
                            from Chicken";
                var chickens = db.Query<Chicken>(sql);
                return chickens;
            }
        }

        public Chicken Get(int chickenId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"select *
                            from Chicken
                            where Id = @ChickenId";
                var parameters = new
                {
                    ChickenId = chickenId
                };
                var chicken = db.QueryFirst<Chicken>(sql, parameters);
                return chicken;
            }
        }

        public IEnumerable<Chicken> GetChickensForTrainer(int trainerId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"select * from Chicken where TrainerId = @TrainerId";
                var parameters = new
                {
                    TrainerId = trainerId
                };

                var chickensForTrainer = db.Query<Chicken>(sql, parameters);
                return chickensForTrainer;
            }
        }

        public void Add(AddChickenCommand chicken)
        {
            throw new NotImplementedException();
        }

        public void Delete(int chickenIdToDelete)
        {
            throw new NotImplementedException();
        }
    }
}
