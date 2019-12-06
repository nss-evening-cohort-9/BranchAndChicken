using BranchAndChicken.Api.Models;

namespace BranchAndChicken.Api.DataAccess
{
    public class UserRepository
    {
        public User Add(User user)
        {
            //sql to do the insert goes here
            user.Id = 22222;
            return user;
        }
    }
}
