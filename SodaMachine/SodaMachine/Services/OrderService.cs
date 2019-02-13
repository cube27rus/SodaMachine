using SodaMachine.Domain.Base;
using SodaMachine.Domain.Models;
using SodaMachine.Domain.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SodaMachine.Services
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork UnitOfWork { get; }
        private ISodaService SodaService { get; }
        private ICoinsService CoinsService { get; }

        public OrderService(IUnitOfWork unitOfWork,
            ISodaService sodaService,
            ICoinsService coinsService)
        {
            UnitOfWork = unitOfWork;
            SodaService = sodaService;
            CoinsService = coinsService;
        }

        public async Task<IEnumerable<Coin>> ProcessOrder(OrderRequest request)
        {
            var coinGroup = request.Coins
                .GroupBy(x => x.Id)
                .Select(y => new
                {
                    Id = y.Key,
                    Count = y.Count()
                });

            foreach (var group in coinGroup)
            {
                var currentCoin = await UnitOfWork.CoinsInMachineRepository.GetFirstOrDefaultAsync(x => x.CoinId == group.Id);

                if (currentCoin != null)
                {
                    currentCoin.Count += group.Count;
                    UnitOfWork.CoinsInMachineRepository.Update(currentCoin);
                    await UnitOfWork.CommitAsync();
                }
                else
                {
                    await UnitOfWork.CoinsInMachineRepository.AddAsync(new CoinsInMachine()
                    {
                        CoinId = group.Id,
                        Count = group.Count,
                        Created = DateTime.Now

                    });
                    await UnitOfWork.CommitAsync();
                }
            }

            // разница между закинутыми монетами и суммой покупки
            var change = request.Coins.Select(x => x.Value).Sum() - request.Sodas.Select(x => x.Price).Sum();
            var calculatedChange = await CoinsService.CalculateChange(change);
            await CoinsService.DeleteCoinsFromMachine(calculatedChange.ToList());
            await SodaService.DeleteSodaFromMachine(request.Sodas.ToList());
            return calculatedChange;
        }
    }
}
