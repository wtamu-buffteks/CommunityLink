using CommunityLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityLink.Pages
{
    public class requestorServicesModel : PageModel
    {
        private readonly CommunityLinkDbContext _context;
        private const int PageSize = 25;

        public requestorServicesModel(CommunityLinkDbContext context)
        {
            _context = context;
        }

        public User? ThisUser { get; set; }
        public Requestor? Requestor { get; set; }
        public List<Request>? Requests { get; set; } = new List<Request>();
        public Request? SelectedRequest { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string CurrentSortColumn { get; set; }
        public string CurrentSortOrder { get; set; }

        [BindProperty]
        public Request FormRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(int? requestId, int pageIndex = 1, string sortOrder = null)
        {
            // Ensure pageIndex is valid
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

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
            else
            {
                Requests = new List<Request>();
                return Page();
            }

            if (ThisUser == null)
            {
                Requests = new List<Request>();
                return Page();
            }
            // The Code Below is necessary as it handles if the user has "unchecked" requestor in MyProfile page
            // If a user has un-deleted requests "unchecking" requestor in MyProfile will delete their requests from the database
            if (ThisUser.Requestor == null)
            {
                Requests = new List<Request>();
                return Page();
            }

            if (requestId.HasValue)
            {
                SelectedRequest = await _context.Requests
                    .Include(r => r.Requestor)
                    .ThenInclude(r => r.User)
                    .FirstOrDefaultAsync(r => r.RequestID == requestId.Value && r.RequestorID == ThisUser.Requestor.RequestorID);
                FormRequest = SelectedRequest;
            }
            else
            {
                var totalRequests = await _context.Requests.CountAsync(r => r.RequestorID == ThisUser.Requestor.RequestorID);
                TotalPages = (int)Math.Ceiling(totalRequests / (double)PageSize);
                CurrentPage = pageIndex;
                if (TotalPages == 0)
                {
                    TotalPages = 1;
                }
                if (CurrentPage > TotalPages) CurrentPage = TotalPages;

                IQueryable<Request> requestsQuery = _context.Requests.Where(r => r.RequestorID == ThisUser.Requestor.RequestorID);

                if (!string.IsNullOrEmpty(sortOrder))
                {
                    var sortParams = sortOrder.Split('_');
                    var sortColumn = sortParams[0];
                    var sortDirection = sortParams[1];

                    if (CurrentSortColumn == sortColumn)
                    {
                        if (CurrentSortOrder == sortDirection)
                        {
                            CurrentSortOrder = sortDirection == "asc" ? "desc" : "asc";
                        }
                        else
                        {
                            CurrentSortOrder = sortDirection;
                        }
                    }
                    else
                    {
                        CurrentSortColumn = sortColumn;
                        CurrentSortOrder = sortDirection;
                    }
                    switch (CurrentSortColumn)
                    {
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

        public async Task<IActionResult> OnPostBackAsync(int pageIndex)
        {
            if (pageIndex < 1) pageIndex = 1;

            var totalRequests = await _context.Requests.CountAsync();
            TotalPages = (int)Math.Ceiling(totalRequests / (double)PageSize);
            CurrentPage = pageIndex;

            if (CurrentPage > TotalPages) CurrentPage = TotalPages;

            Requests = await _context.Requests
                                    .Skip((CurrentPage - 1) * PageSize)
                                    .Take(PageSize)
                                    .ToListAsync();

            SelectedRequest = null;
            return RedirectToPage("/requestorServices", new { pageIndex = CurrentPage });
        }

        public async Task<IActionResult> OnPostAddRequestAsync()
        {
            // Re-retrieve ThisUser to ensure it’s populated
            if (Request.Cookies.TryGetValue("UserID", out string? userIdString) && int.TryParse(userIdString, out int userId))
            {
                ThisUser = await _context.Users.Include(u => u.Requestor)
                                                .FirstOrDefaultAsync(u => u.UserID == userId);
            }
            else if (HttpContext.Session.GetInt32("UserID") is int sessionUserId)
            {
                ThisUser = await _context.Users.Include(u => u.Requestor)
                                                .FirstOrDefaultAsync(u => u.UserID == sessionUserId);
            }

            if (ThisUser == null)
            {
                return RedirectToPage("/Sign-in");
            }

            // Ensure ThisUser is a Requestor
            if (ThisUser.Requestor == null)
            {
                ThisUser.Requestor = new Requestor
                {
                    UserID = ThisUser.UserID,
                    User = ThisUser
                };
                _context.Requestors.Add(ThisUser.Requestor);
                await _context.SaveChangesAsync();  // Ensure Requestor is saved to DB
            }

            // Create and link the new Request
            var newRequest = new Request
            {
                RequestorID = ThisUser.Requestor.RequestorID,
                Requestor = ThisUser.Requestor,
                RequestTitle = FormRequest.RequestTitle,
                RequestDeadline = FormRequest.RequestDeadline,
                RequestDescription = FormRequest.RequestDescription,
                AmountRequested = FormRequest.AmountRequested,
                Category = FormRequest.Category,
                RequestStatus = "Active",
                RequestDate = DateTime.Now
            };

            // Add the request to the context
            _context.Requests.Add(newRequest);
            await _context.SaveChangesAsync();  // Save the new request
            TempData["Message"] = "Request added successfully!";
            return RedirectToPage("/requestorServices");
        }


        public string GetSortOrder(string column)
        {
            if (CurrentSortColumn == column)
            {
                return CurrentSortOrder == "asc" ? $"{column}_desc" : $"{column}_asc";
            }
            return $"{column}_asc";
        }

        public async Task<IActionResult> OnPostUpdateAsync(int requestID)
        {
            var requestToUpdate = await _context.Requests.FindAsync(requestID);

            if (requestToUpdate != null)
            {
                requestToUpdate.RequestTitle = FormRequest.RequestTitle;
                requestToUpdate.RequestDeadline = FormRequest.RequestDeadline;
                requestToUpdate.RequestDescription = FormRequest.RequestDescription;
                requestToUpdate.AmountRequested = FormRequest.AmountRequested;
                requestToUpdate.Category = FormRequest.Category;
                requestToUpdate.RequestStatus = FormRequest.RequestStatus;
                await _context.SaveChangesAsync();
                TempData["Message"] = "Request updated successfully!";
            }
            return RedirectToPage("/requestorServices", new { pageIndex = CurrentPage });
        }


        public IActionResult OnPostDelete(int requestID)
        {
            var request = _context.Requests.Find(requestID);
            if (request != null)
            {
                _context.Requests.Remove(request);
                _context.SaveChanges();
                TempData["Message"] = "Request Deleted successfully!";
            }
            return RedirectToPage();
        }
    }
}