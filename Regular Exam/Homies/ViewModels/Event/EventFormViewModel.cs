namespace Homies.ViewModels.Event
{
    using static Data.DataConstants.Event;
    using System.ComponentModel.DataAnnotations;
    using Homies.ViewModels.Type;

    public class EventFormViewModel
    {
        public EventFormViewModel()
        {
            this.Types = new List<TypeViewModel>();
        }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = InputDateFormat)]
        public DateTime Start { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = InputDateFormat)]
        public DateTime End { get; set; }

        [Required]
        public int TypeId { get; set; }

        public ICollection<TypeViewModel> Types { get; set; }
    }
}