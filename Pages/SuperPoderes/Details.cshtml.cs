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
    public class DetailsModel : PageModel
    {
        private readonly HeroesContext _context;

        public DetailsModel(HeroesContext context)
        {
            _context = context;
        }

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
    }
}
