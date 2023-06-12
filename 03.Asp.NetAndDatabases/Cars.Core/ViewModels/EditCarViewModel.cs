using Cars.Infrastructure.Enums;

namespace Cars.Core.ViewModels
{
    public class EditCarViewModel
    {
        public int Id { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int Year { get; set; }

        public eCarType? Type { get; set; }
    }
}