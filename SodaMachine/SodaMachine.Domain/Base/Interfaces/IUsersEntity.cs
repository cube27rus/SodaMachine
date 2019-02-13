
namespace SodaMachine.Domain.Base.Interfaces
{
    public interface IUsersEntity: IEntity<int>
    {
        int UserId { get; set; }
    }
}
