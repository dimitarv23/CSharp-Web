namespace Cars.Core.ViewModels
{
    using Cars.Infrastructure.Enums;
    using System.ComponentModel.DataAnnotations;

    public class CarViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Make { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string Model { get; set; } = null!;

        [Required]
        [Range(1950, 2023)]
        public int Year { get; set; }

        public eCarType? Type { get; set; }
    }
}