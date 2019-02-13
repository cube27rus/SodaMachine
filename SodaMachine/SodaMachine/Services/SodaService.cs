using SodaMachine.Domain.Base;
using SodaMachine.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SodaMachine.Services
{
    public class SodaService : ISodaService
    {
        private IUnitOfWork UnitOfWork { get; }

        public SodaService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        // удаляет соду из автомата
        public async Task DeleteSodaFromMachine(List<Soda> sodas)
        {
            var groupedSodas = sodas
                .GroupBy(x => x.Id)
                .Select(y => new
                {
                    Id = y.Key,
                    Count = y.Count()
                });

            foreach (var group in groupedSodas)
            {
                var soda = await UnitOfWork.SodaRepository.GetFirstOrDefaultAsync(x => x.Id == group.Id);
                soda.Amount -= group.Count;
                UnitOfWork.SodaRepository.Update(soda);
            }

            await UnitOfWork.CommitAsync();
        }
    }
}
