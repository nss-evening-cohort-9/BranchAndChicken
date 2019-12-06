using BranchAndChicken.Api.DataAccess;
using BranchAndChicken.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BranchAndChicken.Api.Controllers
{
    [Route("api/users")]
    [ApiController,Authorize]
    public class UsersController : FirebaseEnabledController
    {
        [HttpPost]
        public IActionResult AddUser(AddUserCommand command)
        {
            var repository = new UserRepository();

            var user = repository.Add(new User {Email = command.Email, FirebaseUid = UserId});

            return Ok(user);
        }
    }
}