using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LibraryWithBlazorUpdate.Components.Models;
using LibraryWithBlazorUpdate.Data;

namespace LibraryWithBlazorUpdate.Components_Pages_LibraryItemPages
{
    public class DetailsModel : PageModel
    {
        private readonly LibraryWithBlazorUpdate.Data.LibraryWithBlazorUpdateContext _context;

        public DetailsModel(LibraryWithBlazorUpdate.Data.LibraryWithBlazorUpdateContext context)
        {
            _context = context;
        }

        public LibraryItem LibraryItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryitem = await _context.LibraryItems.FirstOrDefaultAsync(m => m.Id == id);

            if (libraryitem is not null)
            {
                LibraryItem = libraryitem;

                return Page();
            }

            return NotFound();
        }
    }
}
