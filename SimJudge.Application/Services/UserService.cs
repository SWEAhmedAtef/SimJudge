using AutoMapper;
using SimJudge.Application.DTOs;
using SimJudge.Domain.Entities;
using SimJudge.Domain.Interfaces;
using SimJudge.Domain.Interfaces.Services;

namespace SimJudge.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _unitOfWork.Users.GetAllAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _unitOfWork.Users.GetByIdAsync(id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            return await _unitOfWork.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var createdUser = await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return createdUser;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user != null)
            {
                await _unitOfWork.Users.DeleteAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _unitOfWork.Users.ExistsAsync(u => u.Id == id);
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _unitOfWork.Users.ExistsAsync(u => u.Email == email);
        }

        public async Task<bool> UserExistsByUserNameAsync(string userName)
        {
            return await _unitOfWork.Users.ExistsAsync(u => u.UserName == userName);
        }
    }
}
