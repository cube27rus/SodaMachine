using System.Collections.Generic;
using System.Threading.Tasks;
using SodaMachine.Domain.Models;

namespace SodaMachine.Services
{
    public interface ISodaService
    {
        Task DeleteSodaFromMachine(List<Soda> sodas);
    }
}