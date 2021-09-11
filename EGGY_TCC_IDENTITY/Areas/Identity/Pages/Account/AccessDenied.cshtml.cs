using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EGGY_TCC_IDENTITY.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class AccessDeniedModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}

