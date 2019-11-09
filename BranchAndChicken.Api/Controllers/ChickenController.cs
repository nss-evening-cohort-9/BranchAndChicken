using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BranchAndChicken.Api.DataAccess;
using BranchAndChicken.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BranchAndChicken.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChickenController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Chicken> GetChickens()
        {
            var repo = new ChickenRepository();
            var chickens = repo.GetAll();
            return chickens;
        }

        [HttpGet("{chickenId}")]
        public Chicken GetChicken(int chickenId)
        {
            var repo = new ChickenRepository();
            var chicken = repo.Get(chickenId);
            return chicken;
        }
    }
}