namespace Homies.Services
{
    using Homies.Contracts;
    using Homies.Data;
    using Homies.Data.Entities;
    using Homies.ViewModels.Event;
    using Homies.ViewModels.Type;
    using Microsoft.EntityFrameworkCore;
    using System.Runtime.InteropServices;

    public class EventService : IEventService
    {
        private readonly HomiesDbContext dbContext;

        public EventService(HomiesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(EventFormViewModel model, string organiserId)
        {
            var eventToAdd = new Event()
            {
                Name = model.Name,
                Description = model.Description,
                OrganiserId = organiserId,
                CreatedOn = DateTime.UtcNow,
                Start = model.Start,
                End = model.End,
                TypeId = model.TypeId
            };

            await this.dbContext.Events.AddAsync(eventToAdd);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(EventFormViewModel model, int eventId)
        {
            var eventToEdit = await this.dbContext.Events
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventToEdit == null)
            {
                return;
            }

            eventToEdit.Name = model.Name;
            eventToEdit.Description = model.Description;
            eventToEdit.Start = model.Start;
            eventToEdit.End = model.End;
            eventToEdit.TypeId = model.TypeId;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<EventViewModel>> GetAllAsync()
        {
            List<EventViewModel> events = await this.dbContext.Events
                .Select(e => new EventViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start,
                    Type = e.Type.Name,
                    Organiser = e.Organiser.UserName
                }).ToListAsync();

            return events;
        }

        public async Task<ICollection<EventViewModel>> GetAllJoinedAsync(string userId)
        {
            List<EventViewModel> events = await this.dbContext.EventsParticipants
                .Where(ep => ep.HelperId == userId)
                .Select(ep => new EventViewModel()
                {
                    Id = ep.Event.Id,
                    Name = ep.Event.Name,
                    Start = ep.Event.Start,
                    Type = ep.Event.Type.Name,
                    Organiser = ep.Event.Organiser.UserName
                })
                .ToListAsync();

            return events;
        }

        public async Task<ICollection<TypeViewModel>> GetAllTypesAsync()
        {
            List<TypeViewModel> types = await this.dbContext.Types
                .Select(e => new TypeViewModel()
                {
                    Id = e.Id,
                    Name = e.Name
                }).ToListAsync();

            return types;
        }

        public async Task<EventFormViewModel?> GetEventByIdAsync(int eventId)
        {
            var ev = await this.dbContext.Events
                .FirstOrDefaultAsync(e => e.Id == eventId);
            
            return ev == null ? null :
                new EventFormViewModel()
                {
                    Name = ev.Name,
                    Description = ev.Description,
                    Start = ev.Start,
                    End = ev.End,
                    TypeId = ev.TypeId,
                    Types = await this.GetAllTypesAsync()
                };
        }

        public async Task<EventDetailsViewModel?> GetEventDetailsAsync(int eventId)
        {
            var model = await this.dbContext.Events
                .Select(e => new EventDetailsViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Start = e.Start,
                    End = e.End,
                    CreatedOn = e.CreatedOn,
                    Type = e.Type.Name,
                    Organiser = e.Organiser.UserName
                })
                .FirstOrDefaultAsync(e => e.Id == eventId);

            return model;
        }

        public async Task<bool> JoinEventAsync(int eventId, string userId)
        {
            bool isEventExisting = await this.dbContext.Events
                .AnyAsync(e => e.Id == eventId);

            bool isEventJoined = await this.dbContext.EventsParticipants
                .AnyAsync(e => e.HelperId == userId &&
                    e.EventId == eventId);

            if (!isEventExisting || isEventJoined)
            {
                return false;
            }

            EventParticipant entity = new EventParticipant()
            {
                EventId = eventId,
                HelperId = userId
            };

            try
            {
                await this.dbContext.EventsParticipants.AddAsync(entity);
                await this.dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task LeaveEventAsync(int eventId, string userId)
        {
            bool isEventExisting = await this.dbContext.Events
                .AnyAsync(e => e.Id == eventId);

            if (!isEventExisting)
            {
                return;
            }

            var entityToRemove = await this.dbContext.EventsParticipants
                .FirstOrDefaultAsync(e => e.HelperId == userId && e.EventId == eventId);

            if (entityToRemove == null)
            {
                return;
            }

            try
            {
                this.dbContext.EventsParticipants.Remove(entityToRemove);
                await this.dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
            }
        }
    }
}