using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserEntity> Get(Guid id);

        Task<IEnumerable<UserEntity>> GetAll();

        Task<UserEntity> Post(UserEntity user);

        Task<UserEntity> Put(UserEntity user);

        Task<bool> Delete(Guid id);
    }
}