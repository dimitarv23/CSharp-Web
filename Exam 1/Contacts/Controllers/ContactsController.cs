namespace Contacts.Controllers
{
    using Contacts.Data;
    using Contacts.Data.Entities;
    using Contacts.Models.Contacts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;

    [Authorize]
    public class ContactsController : Controller
    {
        private readonly ContactsDbContext dbContext;

        public ContactsController(ContactsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            List<ContactViewModel> contacts = await this.dbContext.Contacts
                .Select(c => new ContactViewModel()
                {
                    ContactId = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    Website = c.Website
                }).ToListAsync();

            return View(contacts);
        }

        [HttpGet]
        public async Task<IActionResult> Team()
        {
            var currUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await this.dbContext.Users.FindAsync(currUserId);

            List<ContactViewModel> contacts = await this.dbContext.ApplicationUsersContacts
                .Where(uc => uc.ApplicationUserId == currUserId)
                .Select(c => new ContactViewModel()
                {
                    ContactId = c.Contact.Id,
                    FirstName = c.Contact.FirstName,
                    LastName = c.Contact.LastName,
                    Email = c.Contact.Email,
                    PhoneNumber = c.Contact.PhoneNumber,
                    Address = c.Contact.Address,
                    Website = c.Contact.Website
                }).ToListAsync();

            return View(contacts);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ContactFormModel model = new ContactFormModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContactFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Contact contactToAdd = new Contact()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Website = model.Website
            };

            await this.dbContext.Contacts.AddAsync(contactToAdd);
            await this.dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> AddToTeam(int contactId)
        {
            var contactToAdd = await this.dbContext.Contacts
                .FirstOrDefaultAsync(c => c.Id == contactId);

            if (contactToAdd == null)
            {
                return BadRequest();
            }

            var currUserId = GetUserId();

            bool isExisting = await this.dbContext.ApplicationUsersContacts
                .AnyAsync(uc => uc.ContactId == contactId &&
                    uc.ApplicationUserId == currUserId);

            if (isExisting)
            {
                return RedirectToAction(nameof(All));
            }

            var entry = new ApplicationUserContact()
            {
                ContactId = contactId,
                ApplicationUserId = currUserId
            };

            await this.dbContext.ApplicationUsersContacts.AddAsync(entry);
            await this.dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromTeam(int contactId)
        {
            var contactToRemove = await this.dbContext.Contacts
                .FirstOrDefaultAsync(c => c.Id == contactId);

            if (contactToRemove == null)
            {
                return BadRequest();
            }

            var currUserId = GetUserId();

            var entry = await this.dbContext.ApplicationUsersContacts
                .FirstOrDefaultAsync(uc => uc.ContactId == contactId &&
                    uc.ApplicationUserId == currUserId);

            if (entry == null)
            {
                return BadRequest();
            }

            this.dbContext.ApplicationUsersContacts.Remove(entry);
            await this.dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Team));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int contactId)
        {
            var contactToEdit = await this.dbContext.Contacts
                .FirstOrDefaultAsync(c => c.Id == contactId);

            if (contactToEdit == null)
            {
                return RedirectToAction(nameof(All));
            }

            ContactFormModel model = new ContactFormModel()
            {
                FirstName = contactToEdit.FirstName,
                LastName = contactToEdit.LastName,
                Email = contactToEdit.Email,
                PhoneNumber = contactToEdit.PhoneNumber,
                Address = contactToEdit.Address,
                Website = contactToEdit.Website
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int contactId, ContactFormModel model)
        {
            var contactToEdit = await this.dbContext.Contacts
                .FindAsync(contactId);

            if (contactToEdit == null)
            {
                return RedirectToAction(nameof(All));
            }

            contactToEdit.FirstName = model.FirstName;
            contactToEdit.LastName = model.LastName;
            contactToEdit.Email = model.Email;
            contactToEdit.PhoneNumber = model.PhoneNumber;
            contactToEdit.Address = model.Address;
            contactToEdit.Website = model.Website;

            await this.dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(All));
        }

        private string GetUserId()
            => this.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}