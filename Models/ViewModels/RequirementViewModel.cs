using System.Collections.Generic;
using ToaPro.Models;

namespace ToaPro.Models.ViewModels
{
    public class RequirementViewModel
    {
        public List<Requirement> Class1Requirements { get; set; } = new List<Requirement>();
        public List<Requirement> Class2Requirements { get; set; } = new List<Requirement>();
        public List<Requirement> Class3Requirements { get; set; } = new List<Requirement>();
        public List<Requirement> Class4Requirements { get; set; } = new List<Requirement>();
    }
}
