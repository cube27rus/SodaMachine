using System.Collections.Generic;
using System.Threading.Tasks;
using SodaMachine.Domain.Models;
using SodaMachine.Domain.Models.Request;

namespace SodaMachine.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Coin>> ProcessOrder(OrderRequest request);
    }
}