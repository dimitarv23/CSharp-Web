namespace Cars.Core.ViewModels
{
    using Cars.Infrastructure.Enums;
    using System.ComponentModel.DataAnnotations;

    public class EngineViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(700, 5000)]
        public int Volume { get; set; }

        [Required]
        [Range(50, 2000)]
        public int Horsepower { get; set; }

        [Required]
        public eFuel Fuel { get; set; }
    }
}