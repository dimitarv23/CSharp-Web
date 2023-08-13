namespace Library.Data.Entities
{
    using static GlobalConstants.ApplicationUser;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;

    [Table("ApplicationUsers")]
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.ApplicationUsersBooks = new List<ApplicationUserBook>();
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

        public virtual ICollection<ApplicationUserBook> ApplicationUsersBooks { get; set; }
    }
}