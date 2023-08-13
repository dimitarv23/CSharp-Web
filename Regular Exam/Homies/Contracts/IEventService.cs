namespace Homies.Contracts
{
    using Homies.ViewModels.Event;
    using Homies.ViewModels.Type;

    public interface IEventService
    {
        public Task<ICollection<EventViewModel>> GetAllAsync();

        public Task<ICollection<EventViewModel>> GetAllJoinedAsync(string userId);

        public Task<ICollection<TypeViewModel>> GetAllTypesAsync();

        public Task AddAsync(EventFormViewModel model, string organiserId);

        public Task EditAsync(EventFormViewModel model, int eventId);

        public Task<EventDetailsViewModel?> GetEventDetailsAsync(int eventId);

        public Task<EventFormViewModel?> GetEventByIdAsync(int eventId);

        public Task<bool> JoinEventAsync(int eventId, string userId);

        public Task LeaveEventAsync(int eventId, string userId);
    }
}