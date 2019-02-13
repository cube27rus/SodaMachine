using System.Collections.Generic;

namespace SodaMachine.Domain.Models.Request
{
    public class OrderRequest
    {
       public IEnumerable<Coin> Coins { get; set; }
       public IEnumerable<Soda> Sodas { get; set; }
    }
}
