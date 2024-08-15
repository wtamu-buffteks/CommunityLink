using CommunityLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityLink.Pages {
    public class UserManagementModel : PageModel {
        private readonly CommunityLinkDbContext _context;
        private const int PageSize = 25;

        public UserManagementModel(CommunityLinkDbContext context) {
            _context = context;
        }

        public User? ThisUser { get; set; }
        public List<User>? Users { get; set; }
        public User? SelectedUser { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string CurrentSortColumn { get; set; }
        public string CurrentSortOrder { get; set; }
        public string SelectedUserType { get; set; } = string.Empty;

        [BindProperty]
        public User FormUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? userID, int pageIndex = 1, string sortOrder = null, string SelectedUserType = "") {
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
                // Handle case where user is not found in the database or they don't have access
                return RedirectToPage("/Index");
            }

            IQueryable<User> usersQuery = _context.Users
                                    .Include(u => u.Employee)
                                    .Include(u => u.Volunteer)
                                    .Include(u => u.Requestor);

            if (userID.HasValue) {
                // Load the selected request details
                SelectedUser = await usersQuery
                    .Include(u => u.Employee)
                    .Include(u => u.Volunteer)
                    .Include(u => u.Requestor)
                    .FirstOrDefaultAsync(u => u.UserID == userID.Value);
                FormUser = SelectedUser;
            } else {
                // Apply filtering by user type
                if (!string.IsNullOrEmpty(SelectedUserType)) {
                    this.SelectedUserType = SelectedUserType;

                    if (SelectedUserType == "Employee") {
                        usersQuery = usersQuery.Where(u => u.Employee != null);
                    } else if (SelectedUserType == "Volunteer") {
                        usersQuery = usersQuery.Where(u => u.Volunteer != null);
                    } else if (SelectedUserType == "Requestor") {
                        usersQuery = usersQuery.Where(u => u.Requestor != null);
                    }
                }
                // Pagination logic
                var totalUsers = await usersQuery.CountAsync();
                TotalPages = (int)Math.Ceiling(totalUsers / (double)PageSize);
                CurrentPage = pageIndex;

                if (CurrentPage > TotalPages) {
                    CurrentPage = TotalPages;
                } 

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
                        case "username":
                            usersQuery = CurrentSortOrder == "asc" ? usersQuery.OrderBy(u => u.Username) : usersQuery.OrderByDescending(u => u.Username);
                            break;
                        case "status":
                            usersQuery = CurrentSortOrder == "asc" ? usersQuery.OrderBy(u => u.UserStatus) : usersQuery.OrderByDescending(u => u.UserStatus);
                            break;
                        default:
                            usersQuery = usersQuery.OrderBy(u => u.Username);
                            break;
                    }
                } else {
                    usersQuery = usersQuery.OrderBy(u => u.Username);
                }

                // Load the list of requests with pagination and sorting
                Users = await usersQuery
                                .Skip((CurrentPage - 1) * PageSize)
                                .Take(PageSize)
                                .ToListAsync();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostBackAsync(int pageIndex) {
            // Ensure pageIndex is valid
            if (pageIndex < 1) {
                pageIndex = 1;
            }

            var totalUsers = await _context.Users.CountAsync();
            TotalPages = (int)Math.Ceiling(totalUsers / (double)PageSize);
            CurrentPage = pageIndex;

            if (CurrentPage > TotalPages) {
                CurrentPage = TotalPages;
            }

            // Load the list of requests with pagination
            Users = await _context.Users
                                    .Skip((CurrentPage - 1) * PageSize)
                                    .Take(PageSize)
                                    .ToListAsync();

            SelectedUser = null;
            return RedirectToPage("/UserManagement", new { pageIndex = CurrentPage });
        }

        public string GetSortOrder(string column) {
            if (CurrentSortColumn == column) {
                return CurrentSortOrder == "asc" ? $"{column}_desc" : $"{column}_asc";
            }
            return $"{column}_asc";
        }

        public async Task<IActionResult> OnPostUpdateAsync(int userID) {
            var userToUpdate = await _context.Users.FindAsync(userID);

            if (userToUpdate != null) {
                userToUpdate.Username = FormUser.Username;
                userToUpdate.Email = FormUser.Email;
                userToUpdate.PhoneNumber = FormUser.PhoneNumber;
                userToUpdate.UserStatus = FormUser.UserStatus;

                await _context.SaveChangesAsync();
                TempData["Message"] = "User updated successfully!";
            }
            return RedirectToPage(new { userID });
        }

        public IActionResult OnPostDelete(int userID) {
            var user = _context.Users.Find(userID);
            if (user != null) {
                _context.Users.Remove(user);
                _context.SaveChanges();
                TempData["Message"] = "User deleted successfully!";
            }
            return RedirectToPage();
        }
    }
}
