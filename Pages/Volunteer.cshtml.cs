using CommunityLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityLink.Pages
{
    public class VolunteerModel : PageModel
    {
        private readonly CommunityLinkDbContext _context;
        private const int PageSize = 25;

        public VolunteerModel(CommunityLinkDbContext context)
        {
            _context = context;
        }

        public User? ThisUser { get; set; }
        public List<Request>? Requests { get; set; }
        public Request? SelectedRequest { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string CurrentSortColumn { get; set; }
        public string CurrentSortOrder { get; set; }

        public async Task<IActionResult> OnGetAsync(int? requestId, int pageIndex = 1, string sortOrder = null)
        {
            // Ensure pageIndex is valid
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            // Load user if signed in (but no redirect if not)
            if (Request.Cookies.TryGetValue("UserID", out string? userIdString) && int.TryParse(userIdString, out int userId))
            {
                ThisUser = await _context.Users.Include(u => u.Volunteer)
                                               .Include(u => u.Employee)
                                               .Include(u => u.Requestor)
                                               .FirstOrDefaultAsync(u => u.UserID == userId);
            }
            else if (HttpContext.Session.GetInt32("UserID") is int sessionUserId)
            {
                ThisUser = await _context.Users.Include(u => u.Volunteer)
                                               .Include(u => u.Employee)
                                               .Include(u => u.Requestor)
                                               .FirstOrDefaultAsync(u => u.UserID == sessionUserId);
            }

            // Load request details if requestId is provided
            if (requestId.HasValue)
            {
                SelectedRequest = await _context.Requests
                    .Include(r => r.Requestor)
                    .ThenInclude(r => r.User)
                    .FirstOrDefaultAsync(r => r.RequestID == requestId.Value);
            }
            else
            {
                // Pagination logic
                var totalRequests = await _context.Requests.CountAsync();
                TotalPages = (int)Math.Ceiling(totalRequests / (double)PageSize);
                CurrentPage = pageIndex;

                if (CurrentPage > TotalPages)
                {
                    CurrentPage = TotalPages;
                }

                IQueryable<Request> requestsQuery = _context.Requests;

                // Sorting logic
                if (!string.IsNullOrEmpty(sortOrder))
                {
                    var sortParams = sortOrder.Split('_');
                    var sortColumn = sortParams[0];
                    var sortDirection = sortParams[1];

                    switch (sortColumn)
                    {
                        case "title":
                            requestsQuery = sortDirection == "asc" ? requestsQuery.OrderBy(r => r.RequestTitle) : requestsQuery.OrderByDescending(r => r.RequestTitle);
                            break;
                        case "date":
                            requestsQuery = sortDirection == "asc" ? requestsQuery.OrderBy(r => r.RequestDate) : requestsQuery.OrderByDescending(r => r.RequestDate);
                            break;
                        case "deadline":
                            requestsQuery = sortDirection == "asc" ? requestsQuery.OrderBy(r => r.RequestDeadline) : requestsQuery.OrderByDescending(r => r.RequestDeadline);
                            break;
                        default:
                            requestsQuery = requestsQuery.OrderBy(r => r.RequestDate);
                            break;
                    }
                }
                else
                {
                    requestsQuery = requestsQuery.OrderBy(r => r.RequestDate);
                }

                Requests = await requestsQuery
                                .Skip((CurrentPage - 1) * PageSize)
                                .Take(PageSize)
                                .ToListAsync();
            }

            return Page();
        }


        public IActionResult OnPostDonate(int requestID)
        {
            return RedirectToPage("/Donation", new { requestID });
        }

        public async Task<IActionResult> OnPostBackAsync(int pageIndex)
        {
            // Ensure pageIndex is valid
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            var totalRequests = await _context.Requests.CountAsync();
            TotalPages = (int)Math.Ceiling(totalRequests / (double)PageSize);
            CurrentPage = pageIndex;

            if (CurrentPage > TotalPages)
            {
                CurrentPage = TotalPages;
            }

            // Load the list of requests with pagination
            Requests = await _context.Requests
                                    .Skip((CurrentPage - 1) * PageSize)
                                    .Take(PageSize)
                                    .ToListAsync();

            SelectedRequest = null;
            return RedirectToPage("/Volunteer", new { pageIndex = CurrentPage });
        }

        public string GetSortOrder(string column)
        {
            if (CurrentSortColumn == column)
            {
                return CurrentSortOrder == "asc" ? $"{column}_desc" : $"{column}_asc";
            }
            return $"{column}_asc";
        }
    }
}