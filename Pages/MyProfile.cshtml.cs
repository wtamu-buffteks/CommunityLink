using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommunityLink.Models;

namespace CommunityLink.Pages;

public class MyPageModel : PageModel
{
    private readonly CommunityLinkDbContext _context;
    private readonly ILogger<MyPageModel> _logger;

    public MyPageModel(CommunityLinkDbContext context, ILogger<MyPageModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void OnGet()
    {
    }

}
