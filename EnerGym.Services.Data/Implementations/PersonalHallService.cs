using EnerGym.Data.Models;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Services.Data.Interfaces;

namespace EnerGym.Services.Data.Implementations
{
    public class PersonalHallService(
        IRepository<Progress, int> progressRepository)
        : IPersonalHallService
    {
        
    }
}
