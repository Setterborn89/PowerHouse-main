using AutoMapper;
using PowerHouse.Server.Interfaces.Repository;
using PowerHouse.Server.Interfaces.Services;
using PowerHouse.Server.Models;
using PowerHouse.Server.Repositories;
using PowerHouse.Shared.DTO;

namespace PowerHouse.Server.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task DeleteUser(Guid id)
        {
            await _userRepository.DeleteUser(id);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<List<UserDTO>> GetAlUsersAsync()
        {
            List<UserDTO> users = _mapper.Map<List<User>, List<UserDTO>>(await _userRepository.GetAllUsersAsync());
            return users;
        }

        public async Task<UserDTO> GetUserById(Guid id)
        {
            UserDTO user = _mapper.Map<User, UserDTO>(await _userRepository.GetUserById(id));
            return user;
        }

        public async Task<UserDTO> GetUserByMail(string mail)
        {
            UserDTO user = _mapper.Map<User, UserDTO>(await _userRepository.GetUserByMail(mail));
            return user;
        }

        public async Task PostOrUpdateUser(RegisterDTO userDTO)
        {
            User user = _mapper.Map<RegisterDTO, User>(userDTO);
            await _userRepository.PostOrUpdateUser(user);
            await _userRepository.SaveChangesAsync();
        }
    }
}
