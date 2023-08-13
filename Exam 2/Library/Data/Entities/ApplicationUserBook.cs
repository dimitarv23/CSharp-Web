namespace Library.Data.Entities
{
    using Microsoft.Build.Framework;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ApplicationUsersBooks")]
    public class ApplicationUserBook
    {
        [Required]
        public string ApplicationUserId { get; set; } = null!;

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        [Required]
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; } = null!;
    }
}