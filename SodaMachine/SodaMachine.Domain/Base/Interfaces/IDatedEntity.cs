using System;

namespace SodaMachine.Domain.Base.Interfaces
{
    public interface IDatedEntity
    {
        DateTime Created { get; set; }
    }
}
