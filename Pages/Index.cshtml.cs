using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommunityLink.Models;

namespace CommunityLink.Pages;

public class IndexModel : PageModel
{
    public int? UserID { get; set; }
    public string? CUsername { get; set; }
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
        }
        else
        {
            UserID = null;
        }

        if (Request.Cookies.TryGetValue("Username", out string? username))
        {
            CUsername = username;
        }
        else
        {
            CUsername = "Guest";
        }

    }
}
