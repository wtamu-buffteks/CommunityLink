using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommunityLink.Models;

namespace CommunityLink.Pages;

public class IndexModel : PageModel
{
    public int? UserID { get; set; }
    public string? Username { get; set; }
    private readonly CommunityLinkDbContext _context;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(CommunityLinkDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void OnGet()
    {
        // Check if the UserID cookie exists
        if (Request.Cookies.TryGetValue("UserID", out string? userIdString) && int.TryParse(userIdString, out int userId))
        {
            UserID = userId;
        } //if ther is no cookie, check the session
        else if (HttpContext.Session.GetInt32("UserID") is int sessionUserId)
        {
            UserID = sessionUserId;
        } //if there is no info in the session, the user isn't signed in
        else
        {
            UserID = null;
        }

        // Check if the Username cookie exists
        if (Request.Cookies.TryGetValue("Username", out string? username))
        {
            Username = username;
        }
        else if (HttpContext.Session.GetString("Username") is string sessionUsername)
        {//if ther is no cookie, check the session
            Username = sessionUsername;
        }//if there is no info in the session, the user isn't signed in
        else
        {
            Username = string.Empty;
        }
    }

}
