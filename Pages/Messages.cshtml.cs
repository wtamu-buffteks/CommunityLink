using CommunityLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityLink.Pages {
    public class MessageModel : PageModel {
    private readonly CommunityLinkDbContext _context;
    private const int PageSize = 25;

    public MessageModel(CommunityLinkDbContext context) {
        _context = context;
    }

    public User? ThisUser { get; set; }
    public List<Message> Messages { get; set; }
    public List<ActionNeeded> ActionsNeeded { get; set; }
    public Message? SelectedMessage { get; set; }
    public ActionNeeded? SelectedActionNeeded { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; } = 1;
    public string CurrentSortColumn { get; set; } = string.Empty;
    public string CurrentSortOrder { get; set; } = string.Empty;
    [BindProperty]
    public Message FormMessage { get; set; }
    [BindProperty]
    public string ReceiverEmail { get; set; } 
    [BindProperty]
    public bool IsActionNeeded { get; set; }

    public string FormMode { get; set; } = "List";

    public async Task<IActionResult> OnGetAsync(int? MessageID, int? ActionNeededId, int pageIndex = 1, string sortOrder = "") {
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

        // Fetch messages with sender's details included (sender is a User)
        Messages = await _context.Messages
                        .Where(m => m.ReceiverID == ThisUser.UserID)
                        .Include(m => m.Sender) // Include the User who sent the message
                        .OrderBy(m => m.Read)   // Order by Read status: unread first
                        .ToListAsync();

        // Fetch actions needed with employee details included (if available)
        ActionsNeeded = await _context.ActionNeededs
                                    .Where(a => a.UserID == ThisUser.UserID)
                                    .Include(a => a.Employee)  // Include the Employee details if available
                                    .ThenInclude(e => e.User)  // Include their user info
                                    .ToListAsync();

        if (MessageID.HasValue) {
            SelectedMessage = await _context.Messages
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.MessageID == MessageID.Value);
            
            if (SelectedMessage != null && !SelectedMessage.Read) {
                // Mark the message as read
                SelectedMessage.Read = true;
                await _context.SaveChangesAsync(); // Save the change to the database
            }
            FormMode = "View";
        } else if (ActionNeededId.HasValue) {
            SelectedActionNeeded = await _context.ActionNeededs
                .Include(a => a.Employee)
                .ThenInclude(e => e.User)
                .FirstOrDefaultAsync(a => a.ActionNeededId == ActionNeededId.Value);
            FormMode = "View";
        } else {
            var combinedList = new List<dynamic>();
             // Add ActionsNeeded first
            combinedList.AddRange(ActionsNeeded.Select(a => new {
                Type = "ActionNeeded",
                a.ActionNeededId,
                Sender = a.Employee != null ? a.Employee.User.Username : "System",
                Title = a.Title,
                Message = a.ActionMessage,
                IsRead = false,  // ActionsNeeded are treated as unread until resolved
            }));

            // Add Messages next
            combinedList.AddRange(Messages.Select(m => new {
                Type = "Message",
                m.MessageID,
                Sender = m.Sender?.Username ?? "¿?¿?¿?¿?¿?¿?¿?¿?¿?¿?",
                Title = m.Title,
                Message = m.SenderMessage,
                IsRead = m.Read
            }));

            // Apply sorting
            combinedList = sortOrder switch {
                "Sender_asc" => combinedList.OrderBy(item => item.Sender).ToList(),
                "Sender_desc" => combinedList.OrderByDescending(item => item.Sender).ToList(),
                "Title_asc" => combinedList.OrderBy(item => item.Title).ToList(),
                "Title_desc" => combinedList.OrderByDescending(item => item.Title).ToList(),
                _ => combinedList,
            };
            // Pagination logic
            var totalItems = combinedList.Count();
            TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
            CurrentPage = pageIndex;

            if (CurrentPage > TotalPages)
            {
                CurrentPage = TotalPages;
            }

            if (CurrentPage < 1)
            {
                CurrentPage = 1;
            }

            // Paginate the combined list
            var paginatedList = combinedList
                                    .Skip((CurrentPage - 1) * PageSize)
                                    .Take(PageSize)
                                    .ToList();

            foreach (var item in paginatedList) {
                Console.WriteLine(item.Type);
            }

            ViewData["PaginatedItems"] = paginatedList;
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

        if (ThisUser == null) {
            return RedirectToPage("/Index");
        }

        // Fetch messages with sender's details included (sender is a User)
        Messages = await _context.Messages
                                .Where(m => m.ReceiverID == ThisUser.UserID)
                                .Include(m => m.Sender) // Include the User who sent the message
                                .ToListAsync();

        // Fetch actions needed with employee details included (if available)
        ActionsNeeded = await _context.ActionNeededs
                                    .Where(a => a.UserID == ThisUser.UserID)
                                    .Include(a => a.Employee)  // Include the Employee details if available
                                    .ThenInclude(e => e.User)  // Include their user info
                                    .ToListAsync();
        FormMode = "Send";
        return Page();
    }

    public async Task<IActionResult> OnPostBackAsync(int pageIndex) {
        Console.WriteLine("pageIndex:" + pageIndex);
        // Ensure pageIndex is valid
        if (pageIndex < 1) {
            pageIndex = 1;
        }
        Console.WriteLine("pageIndex:" + pageIndex);

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

        if (ThisUser == null) {
            return RedirectToPage("/Index");
        }


         // Fetch messages with sender's details included (sender is a User)
        Messages = await _context.Messages
                                .Where(m => m.ReceiverID == ThisUser.UserID)
                                .Include(m => m.Sender) // Include the User who sent the message
                                .ToListAsync();
        Console.WriteLine("Messages:" + Messages);

        // Fetch actions needed with employee details included (if available)
        ActionsNeeded = await _context.ActionNeededs
                                    .Where(a => a.UserID == ThisUser.UserID)
                                    .Include(a => a.Employee)  // Include the Employee details if available
                                    .ThenInclude(e => e.User)  // Include their user info
                                    .ToListAsync();
        Console.WriteLine("ActionsNeeded:" + ActionsNeeded);

        var combinedList = new List<dynamic>();
        Console.WriteLine("combinedList:" + combinedList);
        // Add ActionsNeeded first
        combinedList.AddRange(ActionsNeeded.Select(a => new {
            Type = "ActionNeeded",
            a.ActionNeededId,
            Sender = a.Employee != null ? a.Employee.User.Username : "System",
            Title = a.Title,
            Message = a.ActionMessage,
            IsRead = false,  // ActionsNeeded are treated as unread until resolved
        }));
        Console.WriteLine("combinedList:" + combinedList);

        // Add Messages next
        combinedList.AddRange(Messages.Select(m => new {
            Type = "Message",
            m.MessageID,
            Sender = m.Sender?.Username ?? "¿?¿?¿?¿?¿?¿?¿?¿?¿?¿?",
            Title = m.Title,
            Message = m.SenderMessage,
            IsRead = m.Read
        }));
        Console.WriteLine("combinedList:" + combinedList);

        // Pagination logic
        var totalItems = combinedList.Count();
        TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
        CurrentPage = pageIndex;
        if (CurrentPage > TotalPages)
        {
            CurrentPage = TotalPages;
        }

        if (CurrentPage < 1)
        {
            CurrentPage = 1;
        }

        // Paginate the combined list
        var paginatedList = combinedList
                                    .Skip((CurrentPage - 1) * PageSize)
                                    .Take(PageSize)
                                    .ToList();

        ViewData["PaginatedItems"] = paginatedList;
        FormMode = "List";
        return RedirectToPage("/Messages", new { pageIndex = CurrentPage });
    }


    public async Task<IActionResult> OnPostCreateAsync() {
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

        if (ThisUser == null) {
            return RedirectToPage("/Index");
        }
        var receiver = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == ReceiverEmail);
        if (receiver == null) {
            ModelState.AddModelError("ReceiverEmail", "No user with this email exists.");
            return Page(); // Return to form with error message
        }
        // Set the SenderID and ReceiverID
        Console.WriteLine("Form:" + FormMessage);
        FormMessage.SenderID = ThisUser.UserID;
        Console.WriteLine("SenderID:" + FormMessage.SenderID);
        FormMessage.ReceiverID = receiver.UserID;
        Console.WriteLine("ReceiverID:" + FormMessage.ReceiverID);
        FormMessage.Read = false;
        Console.WriteLine("Read:" + FormMessage.Read);
        FormMessage.Receiver = receiver;
        Console.WriteLine("Receiver:" + FormMessage.Receiver);

        if (ModelState.IsValid) {
            Console.WriteLine("_________________Valid");
            Console.WriteLine(IsActionNeeded);

            if (IsActionNeeded && ThisUser.Employee != null) {
                    // Create an Action Needed
                    var actionNeeded = new ActionNeeded {
                        Title = FormMessage.Title,
                        ActionMessage = FormMessage.SenderMessage,
                        UserID = receiver.UserID,
                        EmployeeID = ThisUser.Employee.EmployeeID
                    };
                    _context.ActionNeededs.Add(actionNeeded);
                } else {
                    // Create a normal message
                    _context.Messages.Add(FormMessage);
                }
            await _context.SaveChangesAsync();

            TempData["Message"] = "Message sent successfully!";
            return RedirectToPage("/Messages"); // Redirect back to the messages page
        }

        foreach (var modelStateKey in ModelState.Keys) {
            var modelStateVal = ModelState[modelStateKey];
            foreach (var error in modelStateVal.Errors) {
                Console.WriteLine($"Validation error in '{modelStateKey}': {error.ErrorMessage}");
            }
        }

        Console.WriteLine("_________________FAIL");
        FormMode = "Send";
        return Page();
    }
    public async Task<IActionResult> OnPostDeleteAsync(int id, string type) {

    if (type == "Message") {
        var message = await _context.Messages.FindAsync(id);
        if (message != null) {
            _context.Messages.Remove(message);
        }
    } else if (type == "ActionNeeded") {
        var actionNeeded = await _context.ActionNeededs.FindAsync(id);
        if (actionNeeded != null) {
            _context.ActionNeededs.Remove(actionNeeded);
        }
    }

    await _context.SaveChangesAsync();

    TempData["Message"] = $"{type} resolved/deleted successfully!";
    return RedirectToPage("/Messages");
}
    public string GetSortOrder(string column) {
        if (CurrentSortColumn == column) {
            return CurrentSortOrder == "asc" ? $"{column}_desc" : $"{column}_asc";
        }
        return $"{column}_asc";
    }
}
}

