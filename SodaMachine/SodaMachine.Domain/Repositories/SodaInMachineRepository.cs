using SodaMachine.Domain.Base;
using SodaMachine.Domain.Models;
using SodaMachine.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine.Domain.Repositories
{
    public class SodaInMachineRepository : EntityBaseRepository<SodaInMachine, int>, ISodaInMachineRepository
    {
        public SodaInMachineRepository(ApplicationContext context)
           : base(context)
        {
        }
    }
}
