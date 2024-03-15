using Application.Crosscuting.DTO.User;
using Application.Crosscuting.Helpers;
using Application.Domain.Entity;

namespace Application.Domain.Service
{
    public interface IUserService
    {
        Task<ServiceResult<UserResponse>> GetUserById ( string id);
        Task<ServiceResult<UserResponse>> CreateUser(UserRequest data);
        Task<ServiceResult<UserResponse>> UpdateUser(UserUpdate data);
        Task<ServiceResult<UserResponse>> UpdatePhotoUser(Byte[] data,string userId);


    }
}
