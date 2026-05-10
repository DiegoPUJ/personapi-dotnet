using Microsoft.EntityFrameworkCore;
using personapi_dotnet.DAO.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.DAO.Repositories
{
    public class TelefonoRepository : ITelefonoRepository
    {
        private readonly PersonaDbContext _context;

        public TelefonoRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Telefono>> GetAllAsync()
        {
            return await _context.Telefonos
                .Include(t => t.DuenioNavigation)
                .ToListAsync();
        }

        public async Task<Telefono?> GetByIdAsync(string num)
        {
            return await _context.Telefonos
                .Include(t => t.DuenioNavigation)
                .FirstOrDefaultAsync(t => t.Num == num);
        }

        public async Task CreateAsync(Telefono telefono)
        {
            _context.Telefonos.Add(telefono);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Telefono telefono)
        {
            _context.Telefonos.Update(telefono);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string num)
        {
            var telefono = await GetByIdAsync(num);

            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string num)
        {
            return await _context.Telefonos.AnyAsync(t => t.Num == num);
        }
    }
}