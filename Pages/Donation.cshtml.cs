using CommunityLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    public class DonationModel : PageModel
    {
        public User? ThisUser { get; set; }
        public Stat? ThisStat { get; set; }
        public Request? ThisRequest { get; set; }

        private readonly ILogger<DonationModel> _logger;
        private readonly CommunityLinkDbContext _context;

        public DonationModel(CommunityLinkDbContext context, ILogger<DonationModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public string DonationFrequency { get; set; } = string.Empty;

        [BindProperty]
        public string GiveAmount { get; set; } = string.Empty;
        [BindProperty]
        public string OtherAmount { get; set; } = string.Empty;
        [BindProperty]
        public string CardNumber { get; set; } = string.Empty;
        [BindProperty]
        public string CardHolderName { get; set; } = string.Empty;
        [BindProperty]
        public string ExpiryDate { get; set; } = string.Empty;
        [BindProperty]
        public string CVC { get; set; } = string.Empty;

        public IActionResult OnGet(int? requestID)
        {
            // Check if the UserID cookie exists
            if (Request.Cookies.TryGetValue("UserID", out string? userIdString) && int.TryParse(userIdString, out int userId))
            {
                // Load user information from the database
                ThisUser = _context.Users.Include(u => u.Volunteer)
                                         .Include(u => u.Stat)
                                         .ThenInclude(s => s.RequestStats)
                                         .FirstOrDefault(u => u.UserID == userId);
            }
            // If there is no cookie, check the session
            else if (HttpContext.Session.GetInt32("UserID") is int sessionUserId)
            {
                // Load user information from the database
                ThisUser = _context.Users.Include(u => u.Volunteer)
                                         .Include(u => u.Stat)
                                         .ThenInclude(s => s.RequestStats)
                                         .FirstOrDefault(u => u.UserID == sessionUserId);
            }
            // If there is no info in the session, the user isn't signed in
            else
            {
                // Redirect to the sign-in page if the user is not signed in
                return RedirectToPage("/Sign-in");
            }

            if (ThisUser == null)
            {
                // Handle case where the user is not found in the database
                return RedirectToPage("/Index");
            }

            // Ensure the user has a Stat record
            if (ThisUser.Stat == null) {
                ThisUser.Stat = new Stat { User = ThisUser };
                _context.Stats.Add(ThisUser.Stat);
                _context.SaveChanges();
            }

            if (requestID == null)
            {
                TempData["Message"] = "No request specified for donation.";
                return RedirectToPage("/Volunteer");
            }

            ThisRequest = _context.Requests.Include(r => r.Requestor)
                                           .ThenInclude(r => r.User)
                                           .FirstOrDefault(r => r.RequestID == requestID.Value);
            if (ThisRequest == null)
            {
                TempData["Message"] = "Request not found.";
                return RedirectToPage("/Volunteer");
            }

            return Page();
        }

        public IActionResult OnPost(int requestID)
        {
            int? userId = null;

            if (Request.Cookies.TryGetValue("UserID", out string? userIdString) && int.TryParse(userIdString, out int parsedUserId)) {
                userId = parsedUserId;
            } else if (HttpContext.Session.GetInt32("UserID") is int sessionUserId) {
                userId = sessionUserId;
            }

            if (userId.HasValue) {
                ThisUser = _context.Users.Include(u => u.Volunteer)
                    .Include(u => u.Stat)
                    .ThenInclude(s => s.RequestStats)
                    .FirstOrDefault(u => u.UserID == userId.Value);
                
                if (ThisUser == null) {
                    return RedirectToPage("/Index");
                }

                ThisStat = ThisUser.Stat;
                ThisRequest = _context.Requests.Include(r => r.Requestor)
                                               .ThenInclude(r => r.User)
                                               .FirstOrDefault(r => r.RequestID == requestID);

                if (ThisStat == null || ThisRequest == null) {
                    TempData["Message"] = "Unable to process the donation.";
                    return RedirectToPage("/Index");
                }

                float donationAmount = 0;
                if (GiveAmount == "Other") {
                    if (!float.TryParse(OtherAmount, out donationAmount)) {
                        TempData["Message"] = "Invalid donation amount.";
                        Console.WriteLine(OtherAmount);
                        Console.WriteLine(donationAmount);
                        return Page();
                    }
                }
                else {
                    donationAmount = float.Parse(GiveAmount);
                }

                ThisRequest.AmountRecieved += donationAmount;

                ThisStat.MoneyDonated += donationAmount;

                RequestStat requestStat = new RequestStat { //was originally just going to have it update entries where the user donates to the same request again, but I think it would be better to have different entries for if the user donates to the same request again. We can easily filter by the old ID to do so
                    Stat = ThisStat,
                    OldRequestID = ThisRequest.RequestID,
                    RequestorUsername = ThisRequest.Requestor.User.Username,
                    AmountDonated = donationAmount,
                    DonationDate = DateTime.Now,
                    RequestTitle = ThisRequest.RequestTitle
                };
                _context.RequestStats.Add(requestStat);
                _context.SaveChanges();
                TempData["Message"] = "Donation processed successfully!";
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
