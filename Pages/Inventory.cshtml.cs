using CommunityLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityLink.Pages {
    public class InventoryManagementModel : PageModel {
    private readonly CommunityLinkDbContext _context;
    private const int PageSize = 25;

    public InventoryManagementModel(CommunityLinkDbContext context) {
        _context = context;
    }

    public User? ThisUser { get; set; }
    public List<Inventory>? Inventory { get; set; }
    public Inventory? SelectedInventory { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; } = 1;
    public string CurrentSortColumn { get; set; } = string.Empty;
    public string CurrentSortOrder { get; set; } = string.Empty;

    [BindProperty]
    public Inventory FormInventory { get; set; }
    public string FormMode { get; set; } = "List";
    [BindProperty]

    public List<Request> Requests { get; set; } = new List<Request>();
    [BindProperty]
    public List<User> Users { get; set; } = new List<User>();
    [BindProperty]
    public int SelectedUserID { get; set; }
    [BindProperty]
    public List<InventoryLocation> InventoryLocations { get; set; } = new List<InventoryLocation>();

    public async Task<IActionResult> OnGetAsync(int? inventoryID, int pageIndex = 1, string sortOrder = "") {
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

        //Get Requests, Users, and Locations in case the user is about to edit inventory
        Requests = await _context.Requests.ToListAsync();
        Users = await _context.Users.ToListAsync();
        InventoryLocations = await _context.InventoryLocations.ToListAsync();

        if (Users == null || !Users.Any()) {
                Console.WriteLine("ERROR: NO Users FOUND!");
            }

        if (inventoryID.HasValue) {
            // Load the selected location details
            SelectedInventory  = await _context.Inventory
                .FirstOrDefaultAsync(l => l.InventoryID == inventoryID.Value);
            FormInventory = SelectedInventory;
            FormMode = "Edit";
        } else {
            // Pagination logic
            var totalInventory = await _context.Inventory.CountAsync();
            if (totalInventory == 0) {
                Inventory = new List<Inventory>(); // Initialize an empty list if there is no Inventory
                CurrentPage = 1;
                TotalPages = 1;
            } else {
                TotalPages = (int)Math.Ceiling(totalInventory / (double)PageSize);
                CurrentPage = pageIndex;

                if (CurrentPage > TotalPages) {
                    CurrentPage = TotalPages;
                }

                if (CurrentPage < 1) {
                    CurrentPage = 1;
                }

                IQueryable<Inventory> inventoryQuery = _context.Inventory;

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
                            inventoryQuery = CurrentSortOrder == "asc" ? inventoryQuery.OrderBy(l => l.ItemName) : inventoryQuery.OrderByDescending(l => l.ItemName);
                            break;
                        case "dateReceived":
                            inventoryQuery = CurrentSortOrder == "asc" ? inventoryQuery.OrderBy(l => l.DateReceived) : inventoryQuery.OrderByDescending(l => l.DateReceived);
                            break;
                        default:
                            inventoryQuery = inventoryQuery.OrderBy(l => l.ItemName);
                            break;
                    }
                } else {
                    inventoryQuery = inventoryQuery.OrderBy(l => l.ItemName);
                }

                // Load the list of locations with pagination and sorting
                Inventory = await inventoryQuery
                                    .Skip((CurrentPage - 1) * PageSize)
                                    .Take(PageSize)
                                    .ToListAsync();
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

        Requests = await _context.Requests.ToListAsync();
        Users = await _context.Users.ToListAsync();
        InventoryLocations = await _context.InventoryLocations.ToListAsync();
        FormMode = "Add";
        return Page();
    }

    public async Task<IActionResult> OnPostBackAsync(int pageIndex) {
        // Ensure pageIndex is valid
        if (pageIndex < 1) {
            pageIndex = 1;
        }

        var totalInventory = await _context.Inventory.CountAsync();
        TotalPages = (int)Math.Ceiling(totalInventory / (double)PageSize);
        // so the back button will continue to work even if there is nothing in the database
        if (TotalPages == 0) {
            TotalPages = 1;
        }
        CurrentPage = pageIndex;

        if (CurrentPage > TotalPages) {
            CurrentPage = TotalPages;
        }

        // Load the list of locations with pagination
        Inventory = await _context.Inventory
                                .Skip((CurrentPage - 1) * PageSize)
                                .Take(PageSize)
                                .ToListAsync();

        SelectedInventory = null;
        FormMode = "List";
        return RedirectToPage("/Inventory", new { pageIndex = CurrentPage });
    }

    public async Task<IActionResult> OnPostCreateAsync() {
        if (ModelState.IsValid) {
            Console.WriteLine("_________________Valid");
            _context.Inventory.Add(FormInventory);
            await _context.SaveChangesAsync();

            var user = await _context.Users.Include(u => u.Stat)
                                        .ThenInclude(s => s.DonationStats)
                                        .Include(u => u.Stat)
                                        .ThenInclude(s => s.RequestStats)
                                        .FirstOrDefaultAsync(u => u.UserID == SelectedUserID);

            if (user != null) {
                // Ensure the user has a Stat record
                if (user.Stat == null) {
                    user.Stat = new Stat { User = user };
                    _context.Stats.Add(user.Stat);
                    await _context.SaveChangesAsync();
                }

                // Create or fetch RequestStat
                RequestStat requestStat;
                if (FormInventory.RequestID.HasValue) {
                    var request = await _context.Requests
                                                .Include(r => r.Requestor)
                                                .ThenInclude(rq => rq.User)
                                                .FirstOrDefaultAsync(r => r.RequestID == FormInventory.RequestID.Value);
                    if (request != null) {
                        requestStat = user.Stat.RequestStats.FirstOrDefault(rs => rs.OldRequestID == FormInventory.RequestID.Value);
                        if (requestStat == null) {
                            requestStat = new RequestStat {
                                Stat = user.Stat,
                                OldRequestID = FormInventory.RequestID.Value,
                                RequestorUsername = request.Requestor.User.Username, // Fetch the actual username
                                DonationDate = DateTime.Now,
                                RequestTitle = request.RequestTitle // Fetch the actual request title
                            };
                            user.Stat.RequestStats.Add(requestStat);
                        }
                    } else {
                        requestStat = new RequestStat {
                            Stat = user.Stat,
                            OldRequestID = 0,
                            RequestorUsername = "NoRequest",
                            DonationDate = DateTime.Now,
                            RequestTitle = "No Request"
                        };
                        user.Stat.RequestStats.Add(requestStat);
                    }
                } else {
                    requestStat = new RequestStat {
                        Stat = user.Stat,
                        OldRequestID = 0,
                        RequestorUsername = "NoRequest",
                        DonationDate = DateTime.Now,
                        RequestTitle = "No Request"
                    };
                    user.Stat.RequestStats.Add(requestStat);
                }

                // Save RequestStat to get ID
                await _context.SaveChangesAsync();

                // Create DonationStat
                var donationStat = new DonationStat {
                    Stat = user.Stat,
                    ItemName = FormInventory.ItemName,
                    Quantity = FormInventory.Quantity,
                    DateDonated = DateTime.Now,
                    RequestStat = requestStat
                };

                user.Stat.DonationStats.Add(donationStat);
                requestStat.DonationStats.Add(donationStat);

                await _context.SaveChangesAsync();
            }

            TempData["Message"] = "Inventory added successfully!";
            return RedirectToPage("/Inventory");
        }

        // If the model state is invalid, reload the dropdown data
        Requests = await _context.Requests.ToListAsync();
        Users = await _context.Users.ToListAsync();
        InventoryLocations = await _context.InventoryLocations.ToListAsync();

        foreach (var modelStateKey in ModelState.Keys) {
            var modelStateVal = ModelState[modelStateKey];
            foreach (var error in modelStateVal.Errors) {
                Console.WriteLine($"Validation error in '{modelStateKey}': {error.ErrorMessage}");
            }
        }

        Console.WriteLine("_________________FAIL");
        FormMode = "Add";
        return Page();
    }

    public async Task<IActionResult> OnPostUpdateAsync(int inventoryID) {

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
        
        var inventoryToUpdate = await _context.Inventory.FindAsync(inventoryID);

        if (inventoryToUpdate != null) {
            inventoryToUpdate.ItemName = FormInventory.ItemName;
            inventoryToUpdate.ItemDescription = FormInventory.ItemDescription;
            inventoryToUpdate.Quantity = FormInventory.Quantity;
            inventoryToUpdate.UnitOfMeasurement = FormInventory.UnitOfMeasurement;
            inventoryToUpdate.DateReceived = FormInventory.DateReceived;
            inventoryToUpdate.Barcode = FormInventory.Barcode;
            inventoryToUpdate.StorageType = FormInventory.StorageType;
            inventoryToUpdate.ExpirationDate = FormInventory.ExpirationDate;
            inventoryToUpdate.AtLocation = FormInventory.AtLocation;

            if (ThisUser.Employee.Role == "Admin") {
                inventoryToUpdate.UnitCost = FormInventory.UnitCost;
                inventoryToUpdate.LocationID = FormInventory.LocationID;
                inventoryToUpdate.RequestID = FormInventory.RequestID;
            }

            await _context.SaveChangesAsync();
            TempData["Message"] = "Inventory updated successfully!";
        }
        return RedirectToPage(new { inventoryID });
    }

    public IActionResult OnPostDelete(int inventoryID) {
        var inventory = _context.Inventory.Find(inventoryID);
        if (inventory != null) {
            _context.Inventory.Remove(inventory);
            _context.SaveChanges();
            TempData["Message"] = "Inventory deleted successfully!";
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

