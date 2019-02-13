using SodaMachine.Domain.Base.Interfaces;
using System;

namespace SodaMachine.Domain.Base
{
    public abstract class EntityBase<IdType> : IEntity<IdType>
    {
        public IdType Id { get; set; }
        public DateTime Created { get; set; }
    }
}
