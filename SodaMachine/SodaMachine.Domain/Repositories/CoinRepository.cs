using SodaMachine.Domain.Base;
using SodaMachine.Domain.Models;
using SodaMachine.Domain.Repositories.Interfaces;

namespace SodaMachine.Domain.Repositories
{
    public class CoinRepository : EntityBaseRepository<Coin, int>, ICoinRepository
    {
        public CoinRepository(ApplicationContext context)
           : base(context)
        {
        }
    }
}
