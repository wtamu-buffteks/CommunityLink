using CommunityLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class DonationModel : PageModel
    {
        private readonly ILogger<DonationModel> _logger;
        private readonly CommunityLinkDbContext _context;
        // so for now, I'm going to set this up to donate to the first request.

        public DonationModel(CommunityLinkDbContext context, ILogger<DonationModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public string DonationFrequency { get; set; } = string.Empty;

        [BindProperty]
        public string GiveAmount { get; set; } = string.Empty;

        public IActionResult OnPost()
        {
            _logger.LogInformation("HEYOOOO");
            return Page();
        }
    }
}
