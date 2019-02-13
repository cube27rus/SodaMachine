using SodaMachine.Domain.Base;
using SodaMachine.Domain.Models;
using SodaMachine.Domain.Repositories.Interfaces;

namespace SodaMachine.Domain.Repositories
{
    public class CoinsInMachineRepository : EntityBaseRepository<CoinsInMachine, int>, ICoinsInMachineRepository
    {
        public CoinsInMachineRepository(ApplicationContext context)
           : base(context)
        {
        }
    }
}
