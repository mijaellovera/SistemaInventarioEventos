using ProgramacioIV.DTOs.Auth;

namespace ProgramacioIV.Interfaces
{
    public interface IAuthService
    {
        LoginResponseDTO? Login(LoginDTO loginDTO);
    }
}