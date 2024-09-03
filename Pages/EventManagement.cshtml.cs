using CommunityLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityLink.Pages {
    public class EventManagementModel : PageModel {
    private readonly CommunityLinkDbContext _context;
    private const int PageSize = 25;

    public EventManagementModel(CommunityLinkDbContext context) {
        _context = context;
    }

    public User? ThisUser { get; set; }
    public List<PlannedEvent> Events { get; set; } = new List<PlannedEvent>();
    public PlannedEvent? SelectedEvent { get; set; }
    public List<Inventory>? AvailableInventory { get; set; }
    public List<Inventory>? EventInventory { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; } = 1;
    public string CurrentSortColumn { get; set; } = string.Empty;
    public string CurrentSortOrder { get; set; } = string.Empty;
    [BindProperty]
    public int SelectedInventoryID { get; set; }

    [BindProperty]
    public PlannedEvent FormEvent { get; set; }
    public string FormMode { get; set; } = "List";

    public async Task<IActionResult> OnGetAsync(int? eventID, int pageIndex = 1, string sortOrder = "") {
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

        if (ThisUser == null || ThisUser.Employee == null) {
            // Handle case where user is not found in the database or if they aren't an employee access
            return RedirectToPage("/Index");
        }

        if (eventID.HasValue) {
            SelectedEvent = await _context.PlannedEvents
                    .Include(e => e.Inventory)
                    .FirstOrDefaultAsync(e => e.EventID == eventID.Value);
            
            AvailableInventory = await _context.Inventory
                    .Where(i => i.RequestID == null && i.EventID == null)
                    .ToListAsync();
            
            EventInventory = SelectedEvent?.Inventory;
            FormEvent = SelectedEvent;
            FormMode = "Edit";
        } else {
            // Pagination logic
            var totalEvents = await _context.PlannedEvents.CountAsync();
            if (totalEvents == 0) {
                CurrentPage = 1;
                TotalPages = 1;
            } else {
                TotalPages = (int)Math.Ceiling(totalEvents / (double)PageSize);
                CurrentPage = pageIndex;

                if (CurrentPage > TotalPages) {
                    CurrentPage = TotalPages;
                }

                if (CurrentPage < 1) {
                    CurrentPage = 1;
                }

                IQueryable<PlannedEvent> eventsQuery = _context.PlannedEvents;

                if (!string.IsNullOrEmpty(sortOrder)) {
                    var sortParams = sortOrder.Split('_');
                    var sortColumn = sortParams[0];
                    var sortDirection = sortParams[1];

                    CurrentSortColumn = sortColumn;
                    CurrentSortOrder = sortDirection;

                    eventsQuery = sortColumn switch {
                        "name" => sortDirection == "asc" ? eventsQuery.OrderBy(e => e.EventName) : eventsQuery.OrderByDescending(e => e.EventName),
                        "date" => sortDirection == "asc" ? eventsQuery.OrderBy(e => e.EventDate) : eventsQuery.OrderByDescending(e => e.EventDate),
                        _ => eventsQuery.OrderBy(e => e.EventName),
                    };
                }

                Events = await eventsQuery.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
            }

        }
        return Page();
    }

    public async Task<IActionResult> OnPostAddAsync() {
        // Ensure ThisUser is set correctly
        if (Request.Cookies.TryGetValue("UserID", out string? userIdString) && int.TryParse(userIdString, out int userId)) {
            // Load user information from database
            ThisUser = await _context.Users.Include(u => u.Volunteer)
                                            .Include(u => u.Employee)
                                            .Include(u => u.Requestor)
                                            .FirstOrDefaultAsync(u => u.UserID == userId);
        }
        else if (HttpContext.Session.GetInt32("UserID") is int sessionUserId) {
            // Load user information from database
            ThisUser = await _context.Users.Include(u => u.Volunteer)
                                            .Include(u => u.Employee)
                                            .Include(u => u.Requestor)
                                            .FirstOrDefaultAsync(u => u.UserID == sessionUserId);
        }
        else {
            // Redirect to the sign-in page if the user is not signed in
            return RedirectToPage("/Sign-in");
        }

        // Ensure user is an employee
        if (ThisUser == null || ThisUser.Employee == null) {
            return RedirectToPage("/Index");
        }

        FormMode = "Add";
        return Page();
    }

    public async Task<IActionResult> OnPostCreateAsync() {
        if (ModelState.IsValid) {
            Console.WriteLine("_________________Valid");
            _context.PlannedEvents.Add(FormEvent);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Event added successfully!";
            return RedirectToPage("/EventManagement");
        }

        Console.WriteLine("_________________FAIL");
        FormMode = "Add";
        return Page();
    }

    public async Task<IActionResult> OnPostUpdateAsync(int eventID) {

        // Ensure ThisUser is set correctly
        if (Request.Cookies.TryGetValue("UserID", out string? userIdString) && int.TryParse(userIdString, out int userId)) {
            // Load user information from database
            ThisUser = await _context.Users.Include(u => u.Volunteer)
                                            .Include(u => u.Employee)
                                            .Include(u => u.Requestor)
                                            .FirstOrDefaultAsync(u => u.UserID == userId);
        }
        else if (HttpContext.Session.GetInt32("UserID") is int sessionUserId) {
            // Load user information from database
            ThisUser = await _context.Users.Include(u => u.Volunteer)
                                            .Include(u => u.Employee)
                                            .Include(u => u.Requestor)
                                            .FirstOrDefaultAsync(u => u.UserID == sessionUserId);
        }
        else {
            // Redirect to the sign-in page if the user is not signed in
            return RedirectToPage("/Sign-in");
        }

        // Ensure user is an employee
        if (ThisUser == null || ThisUser.Employee == null) {
            return RedirectToPage("/Index");
        }
        
        var eventToUpdate = await _context.PlannedEvents.FindAsync(eventID);
        if (eventToUpdate != null) {
            eventToUpdate.EventName = FormEvent.EventName;
            eventToUpdate.EventDescription = FormEvent.EventDescription;
            eventToUpdate.EventDate = FormEvent.EventDate;
            eventToUpdate.EventLocation = FormEvent.EventLocation;
            await _context.SaveChangesAsync();
            TempData["Message"] = "Event updated successfully!";
        }

        return RedirectToPage("/EventManagement");
    }

    public IActionResult OnPostDelete(int eventID) {
        var eventToDelete = _context.PlannedEvents.Find(eventID);
        if (eventToDelete != null) {
            _context.PlannedEvents.Remove(eventToDelete);
            _context.SaveChanges();
            TempData["Message"] = "Event deleted successfully!";
        }
        return RedirectToPage("/EventManagement");
    }
    public string GetSortOrder(string column) {
        if (CurrentSortColumn == column) {
            return CurrentSortOrder == "asc" ? $"{column}_desc" : $"{column}_asc";
        }
        return $"{column}_asc";
    }

    public async Task<IActionResult> OnPostAddInventoryAsync(int eventID)
    {
        var eventToUpdate = await _context.PlannedEvents.Include(e => e.Inventory)
                                                        .FirstOrDefaultAsync(e => e.EventID == eventID);

        if (eventToUpdate == null || SelectedInventoryID == 0)
        {
            return RedirectToPage("/EventManagement", new { eventID });
        }

        var inventoryToAdd = await _context.Inventory.FindAsync(SelectedInventoryID);
        if (inventoryToAdd != null)
        {
            inventoryToAdd.EventID = eventID;
            eventToUpdate.Inventory.Add(inventoryToAdd);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("/EventManagement", new { eventID });
    }

    public async Task<IActionResult> OnPostRemoveInventoryAsync(int eventID, int inventoryID)
    {
        var eventToUpdate = await _context.PlannedEvents.Include(e => e.Inventory)
                                                        .FirstOrDefaultAsync(e => e.EventID == eventID);

        if (eventToUpdate == null)
        {
            return RedirectToPage("/EventManagement", new { eventID });
        }

        var inventoryToRemove = eventToUpdate.Inventory.FirstOrDefault(i => i.InventoryID == inventoryID);
        if (inventoryToRemove != null)
        {
            inventoryToRemove.EventID = null;
            eventToUpdate.Inventory.Remove(inventoryToRemove);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("/EventManagement", new { eventID });
    }

    public async Task<IActionResult> OnPostBackAsync(int eventID) {
        // Ensure ThisUser is set correctly
        if (Request.Cookies.TryGetValue("UserID", out string? userIdString) && int.TryParse(userIdString, out int userId)) {
            // Load user information from database
            ThisUser = await _context.Users.Include(u => u.Volunteer)
                                            .Include(u => u.Employee)
                                            .Include(u => u.Requestor)
                                            .FirstOrDefaultAsync(u => u.UserID == userId);
        }
        else if (HttpContext.Session.GetInt32("UserID") is int sessionUserId) {
            // Load user information from database
            ThisUser = await _context.Users.Include(u => u.Volunteer)
                                            .Include(u => u.Employee)
                                            .Include(u => u.Requestor)
                                            .FirstOrDefaultAsync(u => u.UserID == sessionUserId);
        }
        else {
            // Redirect to the sign-in page if the user is not signed in
            return RedirectToPage("/Sign-in");
        }

        // Ensure user is an employee
        if (ThisUser == null || ThisUser.Employee == null) {
            return RedirectToPage("/Index");
        }
        
        var eventToUpdate = await _context.PlannedEvents.FindAsync(eventID);
        if (eventToUpdate != null) {
            eventToUpdate.EventName = FormEvent.EventName;
            eventToUpdate.EventDescription = FormEvent.EventDescription;
            eventToUpdate.EventDate = FormEvent.EventDate;
            eventToUpdate.EventLocation = FormEvent.EventLocation;
            await _context.SaveChangesAsync();
            TempData["Message"] = "Event updated successfully!";
        }

        return RedirectToPage("/EventManagement");
    }

}
}

