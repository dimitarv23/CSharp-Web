namespace Library.Controllers
{
    using Library.Data;
    using Library.Data.Entities;
    using Library.Models.Books;
    using Library.Models.Category;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;

    [Authorize]
    public class BooksController : Controller
    {
        private readonly LibraryDbContext dbContext;

        public BooksController(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            List<BookViewModel> books = await this.dbContext.Books
                .Select(b => new BookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                    Category = b.Category.Name
                })
                .ToListAsync();

            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var currUserId = GetUserId();

            List<BookViewModel> books = await this.dbContext.ApplicationUsersBooks
                .Where(b => b.ApplicationUserId == currUserId)
                .Select(b => new BookViewModel()
                {
                    Id = b.Book.Id,
                    Title = b.Book.Title,
                    Author = b.Book.Author,
                    Description = b.Book.Description,
                    ImageUrl = b.Book.ImageUrl,
                    Rating = b.Book.Rating,
                    Category = b.Book.Category.Name
                })
                .ToListAsync();

            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int bookId)
        {
            BookFormModel model = new BookFormModel()
            {
                Categories = await this.dbContext.Categories
                    .Select(c => new CategoryViewModel()
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await this.dbContext.Categories
                    .Select(c => new CategoryViewModel()
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToListAsync();

                return View(model);
            }

            var bookToAdd = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                CategoryId = model.CategoryId
            };

            await this.dbContext.Books.AddAsync(bookToAdd);
            await this.dbContext.SaveChangesAsync();

            return RedirectToAction("All");
        }

        public async Task<IActionResult> AddToCollection(int bookId)
        {
            var currUserId = GetUserId();

            var book = await this.dbContext.Books
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                return BadRequest();
            }

            bool isExisting = await this.dbContext.ApplicationUsersBooks
                .AnyAsync(b => b.ApplicationUserId == currUserId &&
                    b.BookId == bookId);

            if (isExisting)
            {
                return RedirectToAction("All");
            }

            var entityToAdd = new ApplicationUserBook()
            {
                ApplicationUserId = currUserId,
                BookId = bookId
            };

            await this.dbContext.ApplicationUsersBooks
                .AddAsync(entityToAdd);
            await this.dbContext.SaveChangesAsync();

            return RedirectToAction("All");
        }

        public async Task<IActionResult> RemoveFromCollection(int bookId)
        {
            var currUserId = GetUserId();

            var book = await this.dbContext.Books
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                return BadRequest();
            }

            var entityToRemove = await this.dbContext.ApplicationUsersBooks
                .FirstOrDefaultAsync(b => b.BookId == bookId &&
                    b.ApplicationUserId == currUserId);

            if (entityToRemove == null)
            {
                return RedirectToAction("All");
            }

            this.dbContext.ApplicationUsersBooks
                .Remove(entityToRemove);
            await this.dbContext.SaveChangesAsync();

            return RedirectToAction("Mine");
        }

        private string GetUserId()
            => this.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}