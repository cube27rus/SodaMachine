using System.Collections.Generic;
using System.Threading.Tasks;
using SodaMachine.Domain.Models;

namespace SodaMachine.Services
{
    public interface ICoinsService
    {
        Task DeleteCoinsFromMachine(List<Coin> coins);
        Task<List<Coin>> CalculateChange(decimal change);
    }
}