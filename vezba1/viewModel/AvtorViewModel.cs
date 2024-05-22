using vezba1.Models;

namespace vezba1.viewModel
{
    public class AvtorViewModel
    {
        public IList<Avtor> Avtori { get; set; }
        public string SearchStringI { get; set; }
        public string SearchStringP { get; set; }
        public string SearchStringN { get; set; }
    }
}
