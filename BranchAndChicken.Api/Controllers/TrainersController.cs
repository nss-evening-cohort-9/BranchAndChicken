using System;
using BranchAndChicken.Api.DataAccess;
using BranchAndChicken.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BranchAndChicken.Api.Commands;
using Microsoft.AspNetCore.Authorization;

namespace BranchAndChicken.Api.Controllers
{
    [Route("api/trainers")]
    [ApiController,Authorize]
    public class TrainersController : FirebaseEnabledController
    {
        readonly TrainerRepository _repo;

        public TrainersController(TrainerRepository repo)
        {
            _repo = repo;
        }

        [HttpGet,AllowAnonymous]
        public ActionResult<IEnumerable<Trainer>> GetAllTrainers()
        {
            return _repo.GetAll();
        }

        [HttpGet("{name}")]
        public ActionResult<Trainer> GetByName(string name)
        {
            var trainer = _repo.Get(name);
            return trainer;
        }

        [HttpGet("{id:int}")]
        public ActionResult<Trainer> GetById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("specialty/{specialty}")]
        public ActionResult<Trainer> GetBySpecialty(string specialty)
        {
            try
            {
                return _repo.GetSpecialty(specialty);
            }
            catch (Exception e)
            {
                // do some stuff to handle the error
                return StatusCode(500);
            }
        }

        [HttpDelete("{name}")]
        public IActionResult DeleteTrainer(string name)
        {
            _repo.Remove(name);

            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateTrainer(UpdateTrainerCommand updatedTrainerCommand, int id)
        {
            var updatedTrainer = new Trainer
            {
                Name = updatedTrainerCommand.Name,
                YearsOfExperience = updatedTrainerCommand.YearsOfExperience,
                Specialty = updatedTrainerCommand.Specialty
            };

            var trainerThatGotUpdated = _repo.Update(updatedTrainer, id);

            if (trainerThatGotUpdated == null)
            {
                return NotFound("Could not update trainer");
            }

            return Ok(trainerThatGotUpdated);
        }

        [HttpPost]
        public IActionResult CreateTrainer(AddTrainerCommand newTrainerCommand)
        {
            var newTrainer = new Trainer
            {
                Id = 1,
                Name = newTrainerCommand.Name,
                YearsOfExperience = newTrainerCommand.YearsOfExperience,
                Specialty = newTrainerCommand.Specialty
            };

            var trainerThatGotCreated = _repo.Add(newTrainer);

            return Created($"api/trainers/{trainerThatGotCreated.Name}", trainerThatGotCreated);
        }
    }
}