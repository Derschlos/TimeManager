using TimeManagerMVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TimeManagerMVC.Models
{
    public class EditUserViewModel
    {
        public TimeManagerUser User { get; set; }
        public IList<SelectListItem> Roles { get; set; }
    }
}
