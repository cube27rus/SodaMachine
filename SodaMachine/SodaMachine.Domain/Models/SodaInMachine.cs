using SodaMachine.Domain.Base;
using SodaMachine.Domain.Base.Interfaces;

namespace SodaMachine.Domain.Models
{
    public class SodaInMachine : EntityBase<int>, IEntity<int>
    {
        public int Count { get; set; }

        public Soda Soda { get; set; }
        public int SodaId { get; set; }               

        public SodaDevice SodaMachine { get; set; }
        public int SodaMachineId { get; set; }
    }
}
