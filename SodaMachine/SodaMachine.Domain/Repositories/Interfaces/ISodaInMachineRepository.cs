using SodaMachine.Domain.Base.Interfaces;
using SodaMachine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine.Domain.Repositories.Interfaces
{
    public interface ISodaInMachineRepository : IEntityRepository<SodaInMachine, int>
    {
    }
}
