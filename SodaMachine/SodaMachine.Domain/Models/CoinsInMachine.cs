using SodaMachine.Domain.Base;
using SodaMachine.Domain.Base.Interfaces;

namespace SodaMachine.Domain.Models
{
    public class CoinsInMachine : EntityBase<int>, IEntity<int>
    {
        public int Count { get; set; }

        public Coin Coin { get; set; }
        public int CoinId { get; set; }
    }
}
