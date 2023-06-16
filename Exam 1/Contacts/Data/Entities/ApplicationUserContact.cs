namespace Contacts.Data.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.Build.Framework;

    [Table("ApplicationUsersContacts")]
    public class ApplicationUserContact
    {
        [Required]
        public string ApplicationUserId { get; set; } = null!;

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        [Required]
        public int ContactId { get; set; }

        [ForeignKey(nameof(ContactId))]
        public Contact Contact { get; set; } = null!;
    }
}