using SodaMachine.Domain.Base;
using SodaMachine.Domain.Base.Interfaces;
using SodaMachine.Domain.Models.Enums;

namespace SodaMachine.Domain.Models
{
    public class Coin : EntityBase<int>, IEntity<int>
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public bool IsAvalible { get; set; }
        public CoinType CoinType { get; set; }
    }
}
