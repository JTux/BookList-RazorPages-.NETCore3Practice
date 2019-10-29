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
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _context;

        public UpsertModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Book = new Book();
            if (id == null)
                return Page();

            Book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (Book == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (Book.Id == 0)
            {
                _context.Books.Add(Book);
            }
            else
            {
                var bookFromDb = await _context.Books.FindAsync(Book.Id);
                bookFromDb.Title = Book.Title;
                bookFromDb.Author = Book.Author;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}