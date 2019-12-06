using Microsoft.AspNetCore.Mvc;

namespace BranchAndChicken.Api.Controllers
{
    public abstract class FirebaseEnabledController : ControllerBase
    {
        protected string UserId => User.FindFirst(x => x.Type == "user_id").Value;
    }
}