namespace Homies.Data.Entities
{
    using static DataConstants.Type;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Types")]
    public class Type
    {
        public Type()
        {
            this.Events = new List<Event>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        public ICollection<Event> Events { get; set; }
    }
}