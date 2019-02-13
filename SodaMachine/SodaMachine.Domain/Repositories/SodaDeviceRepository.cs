using SodaMachine.Domain.Base;
using SodaMachine.Domain.Models;
using SodaMachine.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine.Domain.Repositories
{
    public class SodaDeviceRepository : EntityBaseRepository<SodaDevice, int>, ISodaDeviceRepository
    {
        public SodaDeviceRepository(ApplicationContext context)
            : base(context)
        {
        }
    }
}
