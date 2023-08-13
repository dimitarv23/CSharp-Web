namespace Library.Models.Books
{
    using Library.Models.Category;

    public class BookFormModel
    {
        public BookFormModel()
        {
            this.Categories = new List<CategoryViewModel>();
        }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Rating { get; set; }

        public int CategoryId { get; set; }

        public virtual ICollection<CategoryViewModel> Categories { get; set; }
    }
}