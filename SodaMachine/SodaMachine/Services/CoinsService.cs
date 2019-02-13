using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SodaMachine.Domain.Base;
using SodaMachine.Domain.Models;

namespace SodaMachine.Services
{
    public class CoinsService : ICoinsService
    {
        private IUnitOfWork UnitOfWork { get; }

        public CoinsService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        // удаляет монеты на сдачу из автомата
        public async Task DeleteCoinsFromMachine(List<Coin> coins)
        {
            var coinGroup = coins
                .GroupBy(x => x.Id)
                .Select(y => new
                {
                    Id = y.Key,
                    Count = y.Count()
                });

            foreach (var group in coinGroup)
            {
                var coin = await UnitOfWork.CoinsInMachineRepository.GetFirstOrDefaultAsync(x => x.CoinId == group.Id);
                coin.Count -= group.Count;
                UnitOfWork.CoinsInMachineRepository.Update(coin);
            }

            await UnitOfWork.CommitAsync();
        }

        // ищет монеты на сдачу из автомата
        public async Task<List<Coin>> CalculateChange(decimal change)
        {
            var result = new List<Coin>();
            var moneyChange = change;

            var avalibleCoins = (await UnitOfWork.CoinsInMachineRepository.FindByAsync(x => x.Count != 0))
                .Include(x => x.Coin)
                .OrderByDescending(x => x.Coin.Value);

            // поиск подходящих монет для сдачи
            foreach (var coin in avalibleCoins)
            {
                // если сдача выдана
                if (moneyChange == 0)
                {
                    break;
                }

                if (moneyChange < coin.Coin.Value)
                {
                    continue;
                }

                for (int i = 0; i < coin.Count; i++)
                {
                    if (moneyChange >= coin.Coin.Value)
                    {
                        moneyChange -= coin.Coin.Value;
                        result.Add(coin.Coin);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return result;
        }
    }
}
