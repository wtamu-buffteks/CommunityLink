using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityLink.Models;

namespace CommunityLink.Pages
{
    public class StatisticsModel : PageModel
    {
        private readonly CommunityLinkDbContext _context;

        public StatisticsModel(CommunityLinkDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public List<User> AllUsersWithStats { get; set; } = new List<User>();
        public User? ThisUser { get; set; }

        // Sorting properties for Request Stats
        public string RequestCurrentSort { get; set; }
        public string RequestIDSort { get; set; }
        public string RequestTitleSort { get; set; }
        public string RequestAmountSort { get; set; }
        public string RequestDateSort { get; set; }

        // Sorting properties for Donation Stats
        public string DonationCurrentSort { get; set; }
        public string DonationItemNameSort { get; set; }
        public string DonationQuantitySort { get; set; }
        public string DonationDateSort { get; set; }

        public async Task<IActionResult> OnGetAsync(string requestSortOrder, string donationSortOrder)
        {
            // Retrieve the signed-in user's ID
            if (Request.Cookies.TryGetValue("UserID", out string? userIdString) && int.TryParse(userIdString!, out int userId))
            {
                ThisUser = await _context.Users
                                         .Include(u => u.Stat)
                                         .Include(u => u.Employee)
                                         .Include(u => u.Requestor)
                                         .FirstOrDefaultAsync(u => u.UserID == userId);
            } else if (HttpContext.Session.GetInt32("UserID") is int sessionUserId) {
                ThisUser = await _context.Users
                                         .Include(u => u.Stat)
                                         .Include(u => u.Employee)
                                         .Include(u => u.Requestor)
                                         .FirstOrDefaultAsync(u => u.UserID == sessionUserId);
            } else {
                return RedirectToPage("/Sign-in");
            }

            if (ThisUser == null || ThisUser?.Employee == null)
            {
                return RedirectToPage("/Index");
            }

            AllUsersWithStats = await _context.Users
                                              .Include(u => u.Stat)
                                              .ThenInclude(s => s.RequestStats)
                                              .Include(u => u.Stat)
                                              .ThenInclude(s => s.DonationStats)
                                              .Include(u => u.Volunteer)
                                              .ToListAsync();

            // Load all request stats
            var requestStatsQuery = _context.RequestStats.AsQueryable();
            var donationStatsQuery = _context.DonationStats.AsQueryable();

            // Sorting Request Stats
            RequestCurrentSort = requestSortOrder;
            RequestIDSort = String.IsNullOrEmpty(requestSortOrder) ? "requestid_desc" : "";
            RequestTitleSort = requestSortOrder == "RequestTitle" ? "requesttitle_desc" : "RequestTitle";
            RequestAmountSort = requestSortOrder == "AmountDonated" ? "amountdonated_desc" : "AmountDonated";
            RequestDateSort = requestSortOrder == "DonationDate" ? "donationdate_desc" : "DonationDate";

            switch (requestSortOrder)
            {
                case "RequestTitle":
                    requestStatsQuery = requestStatsQuery.OrderBy(rs => rs.RequestTitle);
                    break;
                case "requesttitle_desc":
                    requestStatsQuery = requestStatsQuery.OrderByDescending(rs => rs.RequestTitle);
                    break;
                case "AmountDonated":
                    requestStatsQuery = requestStatsQuery.OrderBy(rs => rs.AmountDonated);
                    break;
                case "amountdonated_desc":
                    requestStatsQuery = requestStatsQuery.OrderByDescending(rs => rs.AmountDonated);
                    break;
                case "DonationDate":
                    requestStatsQuery = requestStatsQuery.OrderBy(rs => rs.DonationDate);
                    break;
                case "donationdate_desc":
                    requestStatsQuery = requestStatsQuery.OrderByDescending(rs => rs.DonationDate);
                    break;
                default:
                    requestStatsQuery = requestStatsQuery.OrderBy(rs => rs.RequestTitle).ThenBy(rs => rs.DonationDate);
                    break;
            }

            // Sorting Donation Stats
            DonationCurrentSort = donationSortOrder;
            DonationItemNameSort = String.IsNullOrEmpty(donationSortOrder) ? "itemname_desc" : "";
            DonationQuantitySort = donationSortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            DonationDateSort = donationSortOrder == "DateDonated" ? "datedonated_desc" : "DateDonated";

            switch (donationSortOrder)
            {
                case "itemname_desc":
                    donationStatsQuery = donationStatsQuery.OrderByDescending(ds => ds.ItemName);
                    break;
                case "Quantity":
                    donationStatsQuery = donationStatsQuery.OrderBy(ds => ds.Quantity);
                    break;
                case "quantity_desc":
                    donationStatsQuery = donationStatsQuery.OrderByDescending(ds => ds.Quantity);
                    break;
                case "DateDonated":
                    donationStatsQuery = donationStatsQuery.OrderBy(ds => ds.DateDonated);
                    break;
                case "datedonated_desc":
                    donationStatsQuery = donationStatsQuery.OrderByDescending(ds => ds.DateDonated);
                    break;
                default:
                    donationStatsQuery = donationStatsQuery.OrderBy(ds => ds.ItemName);
                    break;
            }

            return Page();
        }
    }
}
