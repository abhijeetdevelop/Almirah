namespace Almirah.Services.Interfaces;

public interface IAuthService
{ 
    Task<bool> AuthenticateAsync();
}