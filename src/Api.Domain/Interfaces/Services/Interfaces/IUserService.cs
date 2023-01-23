using Api.Domain.DTOs.User;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Get(Guid id);

        Task<IEnumerable<UserDto>> GetAll();

        Task<UserDtoCreateResult> Post(UserDtoCreate user);

        Task<UserDtoUpdateResult> Put(UserDtoUpdate user);

        Task<bool> Delete(Guid id);
    }
}