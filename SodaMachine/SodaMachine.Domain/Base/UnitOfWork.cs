using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SodaMachine.Domain.Base.Interfaces;
using SodaMachine.Domain.Repositories.Interfaces;

namespace SodaMachine.Domain.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        protected ApplicationContext DbContext { get; }

        #region Repositories
        public ISodaRepository SodaRepository { get; }
        public ICoinRepository CoinRepository { get; }
        public ICoinsInMachineRepository CoinsInMachineRepository { get; }
        #endregion

        public UnitOfWork(ApplicationContext dbContext,
            ISodaRepository sodaRepository,
            ICoinRepository coinRepository,
            ICoinsInMachineRepository coinsInMachineRepository)
        {
            DbContext = dbContext;
            SodaRepository = sodaRepository;
            CoinRepository = coinRepository;
            CoinsInMachineRepository = coinsInMachineRepository;
        }

        public bool Commit()
        {
            UpdateDates();
            return (DbContext.SaveChanges()) > 0;
        }

        public async Task<bool> CommitAsync()
        {
            UpdateDates();
            return (await DbContext.SaveChangesAsync()) > 0;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        private void UpdateDates()
        {
            var entityBaseType = typeof(EntityBase<int>);
            var entries = DbContext.ChangeTracker.Entries()
                .Where(w => w.Entity.GetType().BaseType.Namespace == entityBaseType.Namespace)
                .ToList();

            foreach (EntityEntry entry in entries)
            {
                var updatedEntity = entry.Entity as IDatedEntity;
                
                switch (entry.State)
                {
                    case EntityState.Modified:
                        var originalValueOfEntityCreated = entry.OriginalValues.GetValue<DateTime>("Created");
                        updatedEntity.Created = originalValueOfEntityCreated;
                        break;
                    case EntityState.Added:
                        updatedEntity.Created = DateTime.Now;
                        break;
                }

            }
        }
    }
}
