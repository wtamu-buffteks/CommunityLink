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

    public void OnGet()
    {

    }
}
