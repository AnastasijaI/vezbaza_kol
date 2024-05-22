using Microsoft.AspNetCore.Mvc.Rendering;
using vezba1.Models;

namespace vezba1.viewModel
{
    public class KnigaZanrViewModel
    {
        public List<Kniga> Knigi { get; set; }
        public SelectList Zanrovi { get; set; }
        public string KnigaZanr { get; set; }
        public string SearchStringN { get; set; }
        public string SearchStringG { get; set; }
        public string SearchStringI { get; set; }
    }
}
