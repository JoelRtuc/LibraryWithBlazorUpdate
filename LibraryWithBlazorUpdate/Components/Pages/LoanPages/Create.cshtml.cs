using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibraryWithBlazorUpdate.Components.Models;
using LibraryWithBlazorUpdate.Data;

namespace LibraryWithBlazorUpdate.Components_Pages_LoanPages
{
    public class CreateModel : PageModel
    {
        private readonly LibraryWithBlazorUpdate.Data.LibraryWithBlazorUpdateContext _context;

        public CreateModel(LibraryWithBlazorUpdate.Data.LibraryWithBlazorUpdateContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["LibraryItemId"] = new SelectList(_context.LibraryItems, "Id", "Discriminator");
        ViewData["MemberId"] = new SelectList(_context.Members, "memberId", "memberId");
            return Page();
        }

        [BindProperty]
        public Loan Loan { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Loans.Add(Loan);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
