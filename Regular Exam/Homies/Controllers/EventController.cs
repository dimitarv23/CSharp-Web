namespace Homies.Controllers
{
    using Homies.Contracts;
    using Homies.ViewModels.Event;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var events = await this.eventService.GetAllAsync();

            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new EventFormViewModel()
            {
                Types = await this.eventService.GetAllTypesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Types = await this.eventService.GetAllTypesAsync();
                return View(model);
            }

            try
            {
                await this.eventService.AddAsync(model, this.GetUserId());
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await this.eventService.GetEventDetailsAsync(id);

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            var events = await this.eventService
                .GetAllJoinedAsync(this.GetUserId());

            return View(events);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            bool isAdded = await this.eventService
                .JoinEventAsync(id, this.GetUserId());

            if (isAdded)
            {
                return RedirectToAction(nameof(Joined));
            }
            else
            {
                return RedirectToAction(nameof(All));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            await this.eventService
                .LeaveEventAsync(id, this.GetUserId());

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var eventToEdit = await this.eventService
                .GetEventByIdAsync(id);

            if (eventToEdit == null)
            {
                return BadRequest();
            }

            return View(eventToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventFormViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await this.eventService.EditAsync(model, id);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                return View(model);
            }
        }

        private string GetUserId()
            => this.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}