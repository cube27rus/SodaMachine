using SodaMachine.Domain.Base.Interfaces;
using SodaMachine.Domain.Models;

namespace SodaMachine.Domain.Repositories.Interfaces
{
    public interface ICoinsInMachineRepository : IEntityRepository<CoinsInMachine, int>
    {
    }
}
