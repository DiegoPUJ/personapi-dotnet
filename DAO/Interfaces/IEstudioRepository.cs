using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.DAO.Interfaces
{
    public interface IEstudioRepository
    {
        Task<IEnumerable<Estudio>> GetAllAsync();
        Task<Estudio?> GetByIdAsync(int idProf, int ccPer);
        Task CreateAsync(Estudio estudio);
        Task UpdateAsync(Estudio estudio);
        Task DeleteAsync(int idProf, int ccPer);
        Task<bool> ExistsAsync(int idProf, int ccPer);
    }
}