using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.DAO.Interfaces
{
    public interface IProfesionRepository
    {
        Task<IEnumerable<Profesion>> GetAllAsync();
        Task<Profesion?> GetByIdAsync(int id);
        Task CreateAsync(Profesion profesion);
        Task UpdateAsync(Profesion profesion);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}