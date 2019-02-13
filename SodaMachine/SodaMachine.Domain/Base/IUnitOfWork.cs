using System;
using System.Threading.Tasks;
using SodaMachine.Domain.Repositories.Interfaces;
using SodaMachine.Domain.Repositories;

namespace SodaMachine.Domain.Base
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
        Task<bool> CommitAsync();

        #region Repositories
        ISodaRepository SodaRepository { get; }
        ICoinRepository CoinRepository { get; }
        ICoinsInMachineRepository CoinsInMachineRepository { get; }
        #endregion
    }
}
