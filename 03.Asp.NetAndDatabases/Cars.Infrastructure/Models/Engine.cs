namespace Cars.Infrastructure.Models
{
    using Cars.Infrastructure.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Engines")]
    public class Engine
    {
        public Engine()
        {
            this.Cars = new List<Car>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string? Model { get; set; }

        [Required]
        [Range(700, 5000)]
        public int Volume { get; set; }

        [Required]
        [Range(50, 2000)]
        public int Horsepower { get; set; }

        [Required]
        public eFuel Fuel { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}