using BranchAndChicken.Api.Models;

namespace BranchAndChicken.Api.Commands
{
    public class AddTrainerCommand
    {
        public string Name { get; set; }
        public int YearsOfExperience { get; set; }
        public Specialty Specialty { get; set; }
    }
}
