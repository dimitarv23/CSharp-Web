namespace Contacts.Data.Entities
{
    using static Data.DataConstants.Contact;
    using System.ComponentModel.DataAnnotations;

    public class Contact
    {
        public Contact()
        {
            this.ApplicationUsersContacts = new List<ApplicationUserContact>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [RegularExpression(PhoneNumberPattern)]
        public string PhoneNumber { get; set; } = null!;

        public string? Address { get; set; }

        [Required]
        [RegularExpression(WebsitePattern)]
        public string Website { get; set; } = null!;

        public virtual ICollection<ApplicationUserContact> ApplicationUsersContacts { get; set; }
    }
}