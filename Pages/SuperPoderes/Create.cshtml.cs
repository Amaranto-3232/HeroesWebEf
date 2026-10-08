using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HeroesWeb.Data;
using HeroesWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroesWeb.Pages_SuperPoderes
{
    public class CreateModel : PageModel
    {
        private readonly HeroesContext _context;

        public CreateModel(HeroesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SuperPoderes SuperPoderes { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["HeroeId"] = new SelectList(
                _context.Heroes,
                "Id",
                "Nombre"
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Verificar que el héroe seleccionado exista
            if (!await _context.Heroes.AnyAsync(h => h.Id == SuperPoderes.HeroeId))
            {
                ModelState.AddModelError(
                    "SuperPoderes.HeroeId",
                    "Seleccione un héroe válido."
                );
            }

            if (!ModelState.IsValid)
            {
                ViewData["HeroeId"] = new SelectList(
                    _context.Heroes,
                    "Id",
                    "Nombre",
                    SuperPoderes.HeroeId
                );

                return Page();
            }

            _context.SuperPoderes.Add(SuperPoderes);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}