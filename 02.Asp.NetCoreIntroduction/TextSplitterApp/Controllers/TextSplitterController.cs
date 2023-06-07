namespace TextSplitterApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TextSplitterApp.ViewModels;

    public class TextSplitterController : Controller
    {
        private static string _words = string.Empty;

        [HttpGet]
        public IActionResult Show()
        {
            if (string.IsNullOrWhiteSpace(_words))
            {
                return View(new TextSplitterViewModel());
            }

            var textSplitter = new TextSplitterViewModel()
            {
                Words = _words
            };

            return View(textSplitter);
        }

        [HttpPost]
        public IActionResult Split(TextSplitterViewModel textSplitter)
        {
            var words = textSplitter.Text
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            _words = string.Join(Environment.NewLine, words);

            return RedirectToAction(nameof(Show));
        }
    }
}