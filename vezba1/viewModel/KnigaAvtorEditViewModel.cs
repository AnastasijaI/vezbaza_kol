using Microsoft.AspNetCore.Mvc.Rendering;
using vezba1.Models;

namespace vezba1.viewModel
{
    public class KnigaAvtorEditViewModel
    {
        public Avtor avtor { get; set; }
        public IEnumerable<int>? SelectedKnigas { get; set; }
        public IEnumerable<SelectListItem>? KnigaList { get; set; }
    }
}
