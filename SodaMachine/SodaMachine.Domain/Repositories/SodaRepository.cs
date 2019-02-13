using SodaMachine.Domain.Base;
using SodaMachine.Domain.Models;
using SodaMachine.Domain.Repositories.Interfaces;

namespace SodaMachine.Domain.Repositories
{
    public class SodaRepository : EntityBaseRepository<Soda, int>, ISodaRepository
    {
        public SodaRepository(ApplicationContext context)
            : base(context)
        {
        }
    }
}
