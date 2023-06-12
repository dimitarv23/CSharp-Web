namespace Cars.Infrastructure.Models
{
    using Cars.Infrastructure.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Cars")]
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Make { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string Model { get; set; } = null!;

        [Required]
        [Range(1950, 2023)]
        public int ProductionYear { get; set; }

        public eCarType? Type { get; set; }

        public int EngineId { get; set; }

        [ForeignKey(nameof(EngineId))]
        public Engine? Engine { get; set; }
    }
}