using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommunityLink.Models;
using System.Security.Cryptography.X509Certificates;

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
    [BindProperty]
    public string PhoneNumber { get; set; } = string.Empty;

    public IActionResult OnPostSignIn()
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == Username);
        if (user != null && user.ValidatePassword(Password))
        {
            // Set session variables If the user blocks the cookie, the program will use this
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("UserID", user.UserID);

            //cookie setup. Will last 7 days
            var options = new CookieOptions {Expires = System.DateTime.Now.AddDays(7)};
            Response.Cookies.Append("Username", user.Username, options);
            Response.Cookies.Append("UserID", user.UserID.ToString(), options);

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
        Console.WriteLine(newUser.PhoneNumber);
        newUser.SetPassword(NewPassword);
        _context.Users.Add(newUser);
        _context.SaveChanges();

        var user = _context.Users.SingleOrDefault(u => u.Username == NewUsername);
        if (user != null) {
            // Set session variables If the user blocks the cookie, the program will use this
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("UserID", user.UserID);

            //cookie setup. Will last 7 days
            var options = new CookieOptions {Expires = System.DateTime.Now.AddDays(7)};
            Response.Cookies.Append("Username", user.Username, options);
            Response.Cookies.Append("UserID", user.UserID.ToString(), options);

            // Handle successful sign-in
            return RedirectToPage("/Index");
        }
        // return to the login page if all fails
        return Page();
    }

    public void OnGet() {
        
    }
}
