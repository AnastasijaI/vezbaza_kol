using Microsoft.AspNetCore.Mvc.Rendering;
using vezba1.Models;

namespace vezba1.viewModel
{
    public class AvtorKnigaEditViewModel
    {
        public Kniga Kniga { get; set; }
        public IEnumerable<int>? SelectedAvtors { get; set; }
        public IEnumerable<SelectListItem>? AvtorList { get; set; }
    }
}
