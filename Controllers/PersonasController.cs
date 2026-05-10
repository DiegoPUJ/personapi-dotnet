using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.DAO.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    public class PersonasController : Controller
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonasController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var personas = await _personaRepository.GetAllAsync();
            return View(personas);
        }

        public async Task<IActionResult> Details(int id)
        {
            var persona = await _personaRepository.GetByIdAsync(id);

            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Persona persona)
        {
            if (ModelState.IsValid)
            {
                await _personaRepository.CreateAsync(persona);
                return RedirectToAction(nameof(Index));
            }

            return View(persona);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var persona = await _personaRepository.GetByIdAsync(id);

            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Persona persona)
        {
            if (id != persona.Cc)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _personaRepository.UpdateAsync(persona);
                return RedirectToAction(nameof(Index));
            }

            return View(persona);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var persona = await _personaRepository.GetByIdAsync(id);

            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _personaRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}