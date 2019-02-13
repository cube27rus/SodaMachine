using SodaMachine.Domain.Base;
using SodaMachine.Domain.Base.Interfaces;

namespace SodaMachine.Domain.Models
{
    public class SodaDevice : EntityBase<int>, IEntity<int>
    {
        SodaInMachine SodaInMachine { get; set; }
        CoinsInMachine CoinsInMachine { get; set; }
    }
}
