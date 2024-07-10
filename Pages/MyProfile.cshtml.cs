using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommunityLink.Models;

namespace CommunityLink.Pages;

public class MyPageModel : PageModel
{
    public User? ThisUser { get; set; }
    private readonly CommunityLinkDbContext _context;
    private readonly ILogger<MyPageModel> _logger;

    public MyPageModel(CommunityLinkDbContext context, ILogger<MyPageModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    [BindProperty]
    public string Username { get; set; } = string.Empty;

    [BindProperty]
    public string CurrentPassword { get; set; } = string.Empty;

    [BindProperty]
    public string NewPassword { get; set; } = string.Empty;

    [BindProperty]
    public string? Email { get; set; }

    [BindProperty]
    public string? PhoneNumber { get; set; }

    [BindProperty]
    public string? FullName { get; set; }

    [BindProperty]
    public string? UserLocation { get; set; }

    [BindProperty]
    public bool IsVolunteer { get; set; }

    [BindProperty]
    public bool IsRequestor { get; set; }

    public IActionResult OnGet()
    {
        // Check if the UserID cookie exists
        if (Request.Cookies.TryGetValue("UserID", out string? userIdString) && int.TryParse(userIdString, out int userId))
        {
            // Load user information from database
            ThisUser = _context.Users.Include(u => u.Volunteer)
                                     .Include(u => u.Employee)
                                     .Include(u => u.Requestor)
                                     .FirstOrDefault(u => u.UserID == userId);
        }
        // if there is no cookie, check the session
        else if (HttpContext.Session.GetInt32("UserID") is int sessionUserId)
        {
            // Load user information from database
            ThisUser = _context.Users.Include(u => u.Volunteer)
                                     .Include(u => u.Employee)
                                     .Include(u => u.Requestor)
                                     .FirstOrDefault(u => u.UserID == sessionUserId);
        }
        // if there is no info in the session, the user isn't signed in
        else
        {
            // Redirect to the home page if the user is not signed in
            return RedirectToPage("/Index");
        }

        if (ThisUser == null)
        {
            // Handle case where user is not found in the database
            return RedirectToPage("/Index");
        }

        // Populates the properties with current data
        Username = ThisUser.Username;
        Email = ThisUser.Email;
        PhoneNumber = ThisUser.PhoneNumber;
        FullName = ThisUser.FullName;
        UserLocation = ThisUser.UserLocation;
        IsVolunteer = ThisUser.Volunteer != null;
        IsRequestor = ThisUser.Requestor != null;

        return Page();
    }

    public IActionResult OnPost()
    {
        int? userId = null;

        if (Request.Cookies.TryGetValue("UserID", out string? userIdString) && int.TryParse(userIdString, out int parsedUserId))
        {
            userId = parsedUserId;
        }
        else if (HttpContext.Session.GetInt32("UserID") is int sessionUserId)
        {
            userId = sessionUserId;
        }

        if (userId.HasValue)
        {
            ThisUser = _context.Users.Include(u => u.Volunteer)
                                     .Include(u => u.Employee)
                                     .Include(u => u.Requestor)
                                     .FirstOrDefault(u => u.UserID == userId.Value);

            if (ThisUser == null)
            {
                return RedirectToPage("/Index");
            }

            // Update info
            ThisUser.Username = Username;
            ThisUser.Email = Email;
            ThisUser.PhoneNumber = PhoneNumber;
            ThisUser.FullName = FullName;
            ThisUser.UserLocation = UserLocation;

            // Update password if current password is correct and new password is provided
            if (!string.IsNullOrEmpty(CurrentPassword) && !string.IsNullOrEmpty(NewPassword) && ThisUser.ValidatePassword(CurrentPassword))
            {
                ThisUser.SetPassword(NewPassword);
            }

            // Update volunteer status
            if (IsVolunteer && ThisUser.Volunteer == null)
            {
                ThisUser.Volunteer = new Volunteer { User = ThisUser };
                _context.Volunteers.Add(ThisUser.Volunteer);
            }
            else if (!IsVolunteer && ThisUser.Volunteer != null)
            {
                Console.WriteLine("Test");
                _context.Volunteers.Remove(ThisUser.Volunteer);
                ThisUser.Volunteer = null;
            }

            // Update requestor status
            if (IsRequestor && ThisUser.Requestor == null)
            {
                ThisUser.Requestor = new Requestor { User = ThisUser };
                _context.Requestors.Add(ThisUser.Requestor);
            }
            else if (!IsRequestor && ThisUser.Requestor != null)
            {
                Console.WriteLine("Test");
                _context.Requestors.Remove(ThisUser.Requestor);
                ThisUser.Requestor = null;
            }

            _context.SaveChanges();
            TempData["Message"] = "Profile updated successfully!";
        }

        return Page();
    }
}
