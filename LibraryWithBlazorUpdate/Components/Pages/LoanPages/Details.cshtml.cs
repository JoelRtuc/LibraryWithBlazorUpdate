using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LibraryWithBlazorUpdate.Components.Models;
using LibraryWithBlazorUpdate.Data;

namespace LibraryWithBlazorUpdate.Components_Pages_LoanPages
{
    public class DetailsModel : PageModel
    {
        private readonly LibraryWithBlazorUpdate.Data.LibraryWithBlazorUpdateContext _context;

        public DetailsModel(LibraryWithBlazorUpdate.Data.LibraryWithBlazorUpdateContext context)
        {
            _context = context;
        }

        public Loan Loan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans.FirstOrDefaultAsync(m => m.Id == id);

            if (loan is not null)
            {
                Loan = loan;

                return Page();
            }

            return NotFound();
        }
    }
}
