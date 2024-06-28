using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CommunityLink.Pages
{
    public class SignOutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Remove cookies
            Response.Cookies.Delete("Username");
            Response.Cookies.Delete("UserID");

            // Clear session
            HttpContext.Session.Clear();

            // Redirect to home page
            return RedirectToPage("/Index");
        }
    }
}
