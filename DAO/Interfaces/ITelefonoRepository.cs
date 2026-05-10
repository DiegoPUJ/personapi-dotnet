using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.DAO.Interfaces
{
    public interface ITelefonoRepository
    {
        Task<IEnumerable<Telefono>> GetAllAsync();
        Task<Telefono?> GetByIdAsync(string num);
        Task CreateAsync(Telefono telefono);
        Task UpdateAsync(Telefono telefono);
        Task DeleteAsync(string num);
        Task<bool> ExistsAsync(string num);
    }
}