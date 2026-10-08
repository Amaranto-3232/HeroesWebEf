using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HeroesWeb.Data;
using HeroesWeb.Models;

namespace HeroesWeb.Pages_SuperPoderes
{
    public class DeleteModel : PageModel
    {
        private readonly HeroesContext _context;

        public DeleteModel(HeroesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SuperPoderes SuperPoderes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superPoder = await _context.SuperPoderes
                .Include(s => s.Heroe)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (superPoder == null)
            {
                return NotFound();
            }

            SuperPoderes = superPoder;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superPoder = await _context.SuperPoderes
                .FindAsync(id);

            if (superPoder != null)
            {
                _context.SuperPoderes.Remove(superPoder);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
