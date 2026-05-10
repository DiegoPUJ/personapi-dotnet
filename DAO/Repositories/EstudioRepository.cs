using Microsoft.EntityFrameworkCore;
using personapi_dotnet.DAO.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.DAO.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        private readonly PersonaDbContext _context;

        public EstudioRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estudio>> GetAllAsync()
        {
            return await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .ToListAsync();
        }

        public async Task<Estudio?> GetByIdAsync(int idProf, int ccPer)
        {
            return await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .FirstOrDefaultAsync(e => e.IdProf == idProf && e.CcPer == ccPer);
        }

        public async Task CreateAsync(Estudio estudio)
        {
            _context.Estudios.Add(estudio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Estudio estudio)
        {
            _context.Estudios.Update(estudio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int idProf, int ccPer)
        {
            var estudio = await GetByIdAsync(idProf, ccPer);

            if (estudio != null)
            {
                _context.Estudios.Remove(estudio);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int idProf, int ccPer)
        {
            return await _context.Estudios
                .AnyAsync(e => e.IdProf == idProf && e.CcPer == ccPer);
        }
    }
}