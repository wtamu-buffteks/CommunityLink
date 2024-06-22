using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommunityLink.Models;

namespace CommunityLink.Pages;

public class SignInModel : PageModel
{
    private readonly CommunityLinkDbContext _context;
    private readonly ILogger<SignInModel> _logger;

    public SignInModel(CommunityLinkDbContext context, ILogger<SignInModel> logger)
    {
        _context = context;
        _logger = logger;
    }
    [BindProperty]
    public required string Username { get; set; }
    [BindProperty]
    public required string Password { get; set; }
    [BindProperty]
    public required string NewUsername { get; set; }
    [BindProperty]
    public required string NewPassword { get; set; }
    [BindProperty]
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    

     public IActionResult OnPostSignIn()
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == Username);
            if (user != null && user.ValidatePassword(Password))
            {
                // Handle successful sign-in
                return RedirectToPage("/Index");
            }

            // Handle failed sign-in
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return Page();
        }
    
    public IActionResult OnPostRegister()
        {
            if (_context.Users.Any(u => u.Username == NewUsername))
            {
                ModelState.AddModelError(string.Empty, "Username already taken.");
                return Page();
            }

            var newUser = new User
            {
                Username = NewUsername,
                Email = Email,
                PhoneNumber = PhoneNumber
            };
            newUser.SetPassword(NewPassword);
            _context.Users.Add(newUser);
            _context.SaveChanges();

            // Handle successful registration
            return RedirectToPage("/Index");
        }

    public void OnGet()
    {

    }
}
