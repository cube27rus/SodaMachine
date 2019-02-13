using SodaMachine.Domain.Base;
using SodaMachine.Domain.Base.Interfaces;

namespace SodaMachine.Domain.Models
{
    public class Soda : EntityBase<int>, IEntity<int>
    {
        public string Name { get; set; }
        public string Img { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
