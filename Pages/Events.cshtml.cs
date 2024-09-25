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
    public class EventsModel : PageModel
    {
        private readonly CommunityLinkDbContext _context;
        private const int PageSize = 10;  // Set a page size

        public EventsModel(CommunityLinkDbContext context)
        {
            _context = context;
        }

        public List<PlannedEvent>? Events { get; set; } = new List<PlannedEvent>();
        public List<PlannedEvent>? RSVPEvents { get; set; } = new List<PlannedEvent>();
        public User? ThisUser { get; set; }
        public string FormMode { get; set; } = "List";  // Default mode
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public string CurrentSortColumn { get; set; } = "name";
        public string CurrentSortOrder { get; set; } = "asc";

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1, string sortOrder = "")
        {
            if (pageIndex < 1) pageIndex = 1;

            // Get the current user if logged in
            if (Request.Cookies.TryGetValue("UserID", out string? userIdString) && int.TryParse(userIdString, out int userId))
            {
                ThisUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            }
            else if (HttpContext.Session.GetInt32("UserID") is int sessionUserId)
            {
                ThisUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == sessionUserId);
            }

            await LoadEventsAsync(pageIndex, sortOrder);

            return Page();
        }

        public async Task<IActionResult> OnPostViewRSVPsAsync(int pageIndex = 1, string sortOrder = "EventName_asc")
        {
            // Get the current user if logged in
            if (Request.Cookies.TryGetValue("UserID", out string? userIdString) && int.TryParse(userIdString, out int userId))
            {
                ThisUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            }
            else if (HttpContext.Session.GetInt32("UserID") is int sessionUserId)
            {
                ThisUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == sessionUserId);
            }

            if (ThisUser == null)
            {
                return RedirectToPage("/Sign-in");
            }

            CurrentPage = pageIndex;
            CurrentSortOrder = sortOrder;

            var sortParams = sortOrder.Split('_');
            CurrentSortColumn = sortParams[0];
            var sortDirection = sortParams.Length > 1 ? sortParams[1] : "asc";

            IQueryable<PlannedEvent> eventsQuery = _context.PlannedEvents
                .Include(e => e.Inventory)
                .Include(e => e.UsersAttending)
                .ThenInclude(ua => ua.User)
                .Where(e => e.UsersAttending.Any(ua => ua.UserID == ThisUser.UserID));

            // Apply sorting
            eventsQuery = sortDirection == "asc" ?
                eventsQuery.OrderBy(e => EF.Property<object>(e, CurrentSortColumn)) :
                eventsQuery.OrderByDescending(e => EF.Property<object>(e, CurrentSortColumn));

            // Get total count before applying pagination
            var totalRSVPEvents = await eventsQuery.CountAsync();

            TotalPages = (int)Math.Ceiling(totalRSVPEvents / (double)PageSize);

            // Apply pagination
            RSVPEvents = await eventsQuery.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();

            FormMode = "ViewRSVPs";

            return Page();
        }

        

        public async Task<IActionResult> OnPostRSVPAsync(int eventID)
        {
            // Get the current user if logged in
            if (Request.Cookies.TryGetValue("UserID", out string? userIdString) && int.TryParse(userIdString, out int userId))
            {
                ThisUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            }
            else if (HttpContext.Session.GetInt32("UserID") is int sessionUserId)
            {
                ThisUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == sessionUserId);
            }

            var eventToRSVP = await _context.PlannedEvents.Include(e => e.UsersAttending)
                                                        .FirstOrDefaultAsync(e => e.EventID == eventID);

            if (eventToRSVP != null)
            {
                if (ThisUser != null)
                {
                    // Check if the user has already RSVPed
                    var userAlreadyRSVPed = eventToRSVP.UsersAttending.Any(u => u.UserID == ThisUser.UserID);
                    if (!userAlreadyRSVPed)
                    {
                        // Ensure that the User property is set
                        eventToRSVP.UsersAttending.Add(new UsersAttending
                        {
                            UserID = ThisUser.UserID,
                            User = ThisUser,  // Set the User property
                            PlannedEvent = eventToRSVP
                        });
                        eventToRSVP.RSVP++;
                    }
                }
                else
                {
                    // Non-signed-in user RSVP
                    eventToRSVP.RSVP++;
                }

                await _context.SaveChangesAsync();
                TempData["Message"] = "RSVP successful!";
            }
            else
            {
                TempData["ErrorMessage"] = "Event not found!";
            }

            return RedirectToPage("/Events");
        }

        private async Task LoadEventsAsync(int pageIndex, string sortOrder)
        {
            var totalEvents = await _context.PlannedEvents.CountAsync();
            TotalPages = (int)Math.Ceiling(totalEvents / (double)PageSize);
            CurrentPage = pageIndex;

            if (CurrentPage > TotalPages) CurrentPage = TotalPages;

            IQueryable<PlannedEvent> eventsQuery = _context.PlannedEvents
                .Include(e => e.Inventory)
                .Include(e => e.UsersAttending)
                    .ThenInclude(ua => ua.User);

            if (!string.IsNullOrEmpty(sortOrder))
            {
                var sortParams = sortOrder.Split('_');
                var sortColumn = sortParams[0];
                var sortDirection = sortParams[1];

                CurrentSortColumn = sortColumn;
                CurrentSortOrder = sortDirection;

                eventsQuery = sortColumn switch
                {
                    "name" => sortDirection == "asc" ? eventsQuery.OrderBy(e => e.EventName) : eventsQuery.OrderByDescending(e => e.EventName),
                    "date" => sortDirection == "asc" ? eventsQuery.OrderBy(e => e.EventDate) : eventsQuery.OrderByDescending(e => e.EventDate),
                    _ => eventsQuery.OrderBy(e => e.EventName),
                };
            }

            Events = await eventsQuery.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
            // Ensure pageIndex is at least 1
            CurrentPage = pageIndex < 1 ? 1 : pageIndex;
        }

        private async Task LoadRSVPEventsAsync(int pageIndex, string sortOrder)
        {
            var totalRSVPEvents = await _context.PlannedEvents
                .Where(e => e.UsersAttending.Any(ua => ua.UserID == ThisUser.UserID))
                .CountAsync();
            TotalPages = (int)Math.Ceiling(totalRSVPEvents / (double)PageSize);
            CurrentPage = pageIndex;

            if (CurrentPage > TotalPages) CurrentPage = TotalPages;

            IQueryable<PlannedEvent> rsvpEventsQuery = _context.PlannedEvents
                .Include(e => e.UsersAttending)
                    .ThenInclude(ua => ua.User)
                .Where(e => e.UsersAttending.Any(ua => ua.UserID == ThisUser.UserID));

            if (!string.IsNullOrEmpty(sortOrder))
            {
                var sortParams = sortOrder.Split('_');
                var sortColumn = sortParams[0];
                var sortDirection = sortParams[1];

                CurrentSortColumn = sortColumn;
                CurrentSortOrder = sortDirection;

                rsvpEventsQuery = sortColumn switch
                {
                    "name" => sortDirection == "asc" ? rsvpEventsQuery.OrderBy(e => e.EventName) : rsvpEventsQuery.OrderByDescending(e => e.EventName),
                    "date" => sortDirection == "asc" ? rsvpEventsQuery.OrderBy(e => e.EventDate) : rsvpEventsQuery.OrderByDescending(e => e.EventDate),
                    _ => rsvpEventsQuery.OrderBy(e => e.EventName),
                };
            }

            RSVPEvents = await rsvpEventsQuery.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
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