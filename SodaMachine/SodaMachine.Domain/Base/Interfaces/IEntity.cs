
namespace SodaMachine.Domain.Base.Interfaces
{
    public interface IEntity<IdType> : IDatedEntity
    {
        IdType Id { get; set; }
    }
}
