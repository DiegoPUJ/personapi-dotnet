using Microsoft.EntityFrameworkCore;
using personapi_dotnet.DAO.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.DAO.Repositories
{
    public class ProfesionRepository : IProfesionRepository
    {
        private readonly PersonaDbContext _context;

        public ProfesionRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profesion>> GetAllAsync()
        {
            return await _context.Profesions.ToListAsync();
        }

        public async Task<Profesion?> GetByIdAsync(int id)
        {
            return await _context.Profesions.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateAsync(Profesion profesion)
        {
            _context.Profesions.Add(profesion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Profesion profesion)
        {
            _context.Profesions.Update(profesion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var profesion = await GetByIdAsync(id);

            if (profesion != null)
            {
                _context.Profesions.Remove(profesion);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Profesions.AnyAsync(p => p.Id == id);
        }
    }
}