using CommunityLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityLink.Pages {
    public class LocationManagementModel : PageModel {
    private readonly CommunityLinkDbContext _context;
    private const int PageSize = 25;

    public LocationManagementModel(CommunityLinkDbContext context) {
        _context = context;
    }

    public User? ThisUser { get; set; }
    public List<InventoryLocation>? Locations { get; set; }
    public InventoryLocation? SelectedLocation { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; } = 1;
    public string CurrentSortColumn { get; set; } = string.Empty;
    public string CurrentSortOrder { get; set; } = string.Empty;

    [BindProperty]
    public InventoryLocation FormLocation { get; set; }
    public string FormMode { get; set; } = "List";

    public async Task<IActionResult> OnGetAsync(int? locationId, int pageIndex = 1, string sortOrder = "") {
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

        if (ThisUser == null || ThisUser.Employee == null || ThisUser.Employee.Role != "Admin") {
            // Handle case where user is not found in the database or they don't have admin access
            return RedirectToPage("/Index");
        }

        if (locationId.HasValue) {
            // Load the selected location details
            SelectedLocation = await _context.InventoryLocations
                .FirstOrDefaultAsync(l => l.LocationID == locationId.Value);
            FormLocation = SelectedLocation;
            FormMode = "Edit";
        } else {
            // Pagination logic
            var totalLocations = await _context.InventoryLocations.CountAsync();
            if (totalLocations == 0) {
                Locations = new List<InventoryLocation>(); // Initialize an empty list if there are no locations
                CurrentPage = 1;
                TotalPages = 1;
            } else {
                TotalPages = (int)Math.Ceiling(totalLocations / (double)PageSize);
                CurrentPage = pageIndex;

                if (CurrentPage > TotalPages) {
                    CurrentPage = TotalPages;
                }

                if (CurrentPage < 1) {
                    CurrentPage = 1;
                }

                IQueryable<InventoryLocation> locationsQuery = _context.InventoryLocations;

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
                    switch (CurrentSortColumn) {
                        case "name":
                            locationsQuery = CurrentSortOrder == "asc" ? locationsQuery.OrderBy(l => l.LocationName) : locationsQuery.OrderByDescending(l => l.LocationName);
                            break;
                        case "address":
                            locationsQuery = CurrentSortOrder == "asc" ? locationsQuery.OrderBy(l => l.LocationAddress) : locationsQuery.OrderByDescending(l => l.LocationAddress);
                            break;
                        default:
                            locationsQuery = locationsQuery.OrderBy(l => l.LocationName);
                            break;
                    }
                } else {
                    locationsQuery = locationsQuery.OrderBy(l => l.LocationName);
                }

                // Load the list of locations with pagination and sorting
                Locations = await locationsQuery
                                    .Skip((CurrentPage - 1) * PageSize)
                                    .Take(PageSize)
                                    .ToListAsync();
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAddAsync() {
        FormMode = "Add";
        return Page();
    }

    public async Task<IActionResult> OnPostBackAsync(int pageIndex) {
        // Ensure pageIndex is valid
        if (pageIndex < 1) {
            pageIndex = 1;
        }

        var totalLocations = await _context.InventoryLocations.CountAsync();
        TotalPages = (int)Math.Ceiling(totalLocations / (double)PageSize);
        CurrentPage = pageIndex;

        if (CurrentPage > TotalPages) {
            CurrentPage = TotalPages;
        }

        // Load the list of locations with pagination
        Locations = await _context.InventoryLocations
                                .Skip((CurrentPage - 1) * PageSize)
                                .Take(PageSize)
                                .ToListAsync();

        SelectedLocation = null;
        FormMode = "List";
        return RedirectToPage("/LocationManagement", new { pageIndex = CurrentPage });
    }

    public async Task<IActionResult> OnPostCreateAsync() {
        if (ModelState.IsValid) {
            _context.InventoryLocations.Add(FormLocation);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Location added successfully!";
            return RedirectToPage("/LocationManagement");
        }
        FormMode = "Add";
        return Page();
    }

    public async Task<IActionResult> OnPostUpdateAsync(int locationId) {
        var locationToUpdate = await _context.InventoryLocations.FindAsync(locationId);

        if (locationToUpdate != null) {
            locationToUpdate.LocationName = FormLocation.LocationName;
            locationToUpdate.LocationAddress = FormLocation.LocationAddress;
            locationToUpdate.Capacity = FormLocation.Capacity;
            locationToUpdate.Cold = FormLocation.Cold;
            locationToUpdate.Frozen = FormLocation.Frozen;

            await _context.SaveChangesAsync();
            TempData["Message"] = "Location updated successfully!";
        }
        return RedirectToPage(new { locationId });
    }

    public IActionResult OnPostDelete(int locationId) {
        var location = _context.InventoryLocations.Find(locationId);
        if (location != null) {
            _context.InventoryLocations.Remove(location);
            _context.SaveChanges();
            TempData["Message"] = "Location deleted successfully!";
        }
        return RedirectToPage();
    }
    public string GetSortOrder(string column) {
        if (CurrentSortColumn == column) {
            return CurrentSortOrder == "asc" ? $"{column}_desc" : $"{column}_asc";
        }
        return $"{column}_asc";
    }
}
}

