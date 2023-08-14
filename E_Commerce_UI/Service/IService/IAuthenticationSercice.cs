using E_Commerce_Models;

namespace E_Commerce_UI.Service.IService
{
    public interface IAuthenticationSercice
    {
        public Task<RegisterResponseDTO> RegisterUser(RegisterRequestDTO requestDTO);
        public Task<LoginResponseDTO> LoginUser(LoginRequestDTO requestDTO);
        Task Logout();
    }
}
