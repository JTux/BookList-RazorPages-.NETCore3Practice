using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.Books
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task OnGetAsync(int id)
        {
            Book = await _context.Books.FindAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var bookFromDb = await _context.Books.FindAsync(Book.Id);
            bookFromDb.Title = Book.Title;
            bookFromDb.Author = Book.Author;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}