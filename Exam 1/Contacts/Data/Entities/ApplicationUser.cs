namespace Contacts.Data.Entities
{
    using static DataConstants.ApplicationUser;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.ApplicationUsersContacts = new List<ApplicationUserContact>();
        }

        [Required]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public override string UserName 
        { 
            get => base.UserName; 
            set => base.UserName = value; 
        }

        [Required]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        public override string Email 
        { 
            get => base.Email; 
            set => base.Email = value; 
        }

        public virtual ICollection<ApplicationUserContact> ApplicationUsersContacts { get; set; }
    }
}