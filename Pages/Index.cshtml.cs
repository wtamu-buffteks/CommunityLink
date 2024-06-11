using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommunityLink.Models;

namespace CommunityLink.Pages;

public class IndexModel : PageModel
{
    private readonly CommunityLinkDbContext _context;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(CommunityLinkDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
