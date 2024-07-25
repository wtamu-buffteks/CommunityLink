using CommunityLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityLink.Pages {
    public class VolunteerModel : PageModel {
        private readonly CommunityLinkDbContext _context;
        private const int PageSize = 25;

        public VolunteerModel(CommunityLinkDbContext context) {
            _context = context;
        }

        public User? ThisUser { get; set; }
        public List<Request>? Requests { get; set; }
        public Request? SelectedRequest { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string CurrentSortColumn { get; set; }
        public string CurrentSortOrder { get; set; }

        public async Task<IActionResult> OnGetAsync(int? requestId, int pageIndex = 1, string sortOrder = null) {
            // Ensure pageIndex is valid
            if (pageIndex < 1){
                pageIndex = 1;
            }

            // Check if the UserID cookie exists
            if (Request.Cookies.TryGetValue("UserID", out string? userIdString) && int.TryParse(userIdString, out int userId)) {
                // Load user information from database
                ThisUser = await _context.Users.Include(u => u.Volunteer)
                                                .Include(u => u.Employee)
                                                .Include(u => u.Requestor)
                                                .FirstOrDefaultAsync(u => u.UserID == userId);
            }
            // If there is no cookie, check the session
            else if (HttpContext.Session.GetInt32("UserID") is int sessionUserId) {
                // Load user information from database
                ThisUser = await _context.Users.Include(u => u.Volunteer)
                                                .Include(u => u.Employee)
                                                .Include(u => u.Requestor)
                                                .FirstOrDefaultAsync(u => u.UserID == sessionUserId);
            }
            // If there is no info in the session, the user isn't signed in
            else {
                // Redirect to the sign-in page if the user is not signed in
                return RedirectToPage("/Sign-in");
            }

            if (ThisUser == null) {
                // Handle case where user is not found in the database
                return RedirectToPage("/Index");
            }

            if (requestId.HasValue) {
                // Load the selected request details
                SelectedRequest = await _context.Requests
                    .Include(r => r.Requestor)
                    .ThenInclude(r => r.User)
                    .FirstOrDefaultAsync(r => r.RequestID == requestId.Value);
            } else {
                // Pagination logic
                var totalRequests = await _context.Requests.CountAsync();
                TotalPages = (int)Math.Ceiling(totalRequests / (double)PageSize);
                CurrentPage = pageIndex;

                if (CurrentPage > TotalPages) {
                    CurrentPage = TotalPages;
                }

                IQueryable<Request> requestsQuery = _context.Requests;
                Console.WriteLine("Sort Order: " + sortOrder);

                if (!string.IsNullOrEmpty(sortOrder)) {
                    var sortParams = sortOrder.Split('_');
                    var sortColumn = sortParams[0];
                    var sortDirection = sortParams[1];

                    if (CurrentSortColumn == sortColumn) {
                        if (CurrentSortOrder == sortDirection) {
                            CurrentSortOrder = sortDirection == "asc" ? "desc" : "asc";
                        } else {
                            CurrentSortOrder = sortDirection;
                        }
                    } else {
                        CurrentSortColumn = sortColumn;
                        CurrentSortOrder = sortDirection;
                    }
                    Console.WriteLine("Sorting:" + CurrentSortOrder);
                    switch (CurrentSortColumn) {
                        case "title":
                            requestsQuery = CurrentSortOrder == "asc" ? requestsQuery.OrderBy(r => r.RequestTitle) : requestsQuery.OrderByDescending(r => r.RequestTitle);
                            break;
                        case "date":
                            requestsQuery = CurrentSortOrder == "asc" ? requestsQuery.OrderBy(r => r.RequestDate) : requestsQuery.OrderByDescending(r => r.RequestDate);
                            break;
                        case "deadline":
                            requestsQuery = CurrentSortOrder == "asc" ? requestsQuery.OrderBy(r => r.RequestDeadline) : requestsQuery.OrderByDescending(r => r.RequestDeadline);
                            break;
                        default:
                            requestsQuery = requestsQuery.OrderBy(r => r.RequestDate);
                            break;
                    }
                } else {
                    requestsQuery = requestsQuery.OrderBy(r => r.RequestDate);
                }

                // Load the list of requests with pagination and sorting
                Requests = await requestsQuery
                                    .Skip((CurrentPage - 1) * PageSize)
                                    .Take(PageSize)
                                    .ToListAsync();
            }

            return Page();
        }

        public IActionResult OnPostDonate(int requestID) {
            return RedirectToPage("/Donation", new { requestID });
        }

        public async Task<IActionResult> OnPostBackAsync(int pageIndex) {
            // Ensure pageIndex is valid
            if (pageIndex < 1) {
                pageIndex = 1;
            }

            var totalRequests = await _context.Requests.CountAsync();
            TotalPages = (int)Math.Ceiling(totalRequests / (double)PageSize);
            CurrentPage = pageIndex;

            if (CurrentPage > TotalPages) {
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

        public string GetSortOrder(string column) {
            if (CurrentSortColumn == column) {
                return CurrentSortOrder == "asc" ? $"{column}_desc" : $"{column}_asc";
            }
            return $"{column}_asc";
        }
    }
}
