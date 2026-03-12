using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LibraryWithBlazorUpdate.Components.Models;
using LibraryWithBlazorUpdate.Data;

namespace LibraryWithBlazorUpdate.Components_Pages_MemberPages
{
    public class DetailsModel : PageModel
    {
        private readonly LibraryWithBlazorUpdate.Data.LibraryWithBlazorUpdateContext _context;

        public DetailsModel(LibraryWithBlazorUpdate.Data.LibraryWithBlazorUpdateContext context)
        {
            _context = context;
        }

        public Member Member { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FirstOrDefaultAsync(m => m.memberId == id);

            if (member is not null)
            {
                Member = member;

                return Page();
            }

            return NotFound();
        }
    }
}
