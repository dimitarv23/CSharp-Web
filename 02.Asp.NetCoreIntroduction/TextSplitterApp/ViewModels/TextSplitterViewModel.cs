namespace TextSplitterApp.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using TextSplitterApp.Common;

    public class TextSplitterViewModel
    {
        [Required(ErrorMessage = GlobalConstants.TextFieldErrorMessage)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = GlobalConstants.StringLengthErrorMessage)]
        public string Text { get; set; } = null!;

        public string Words { get; set; } = null!;
    }
}