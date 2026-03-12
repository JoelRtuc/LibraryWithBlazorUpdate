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
    public class IndexModel : PageModel
    {
        private readonly LibraryWithBlazorUpdate.Data.LibraryWithBlazorUpdateContext _context;

        public IndexModel(LibraryWithBlazorUpdate.Data.LibraryWithBlazorUpdateContext context)
        {
            _context = context;
        }

        public IList<LibraryItem> LibraryItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            LibraryItem = await _context.LibraryItems.ToListAsync();
        }
    }
}
