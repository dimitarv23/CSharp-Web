namespace Cars.Core.ViewModels
{
    using Cars.Infrastructure.Enums;

    public class AddCarViewModel
    {
        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int Year { get; set; }

        public eCarType? Type { get; set; }

        public int EngineId { get; set; }
    }
}