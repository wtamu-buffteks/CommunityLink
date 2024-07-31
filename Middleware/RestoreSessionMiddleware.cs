using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using CommunityLink.Models;
using Microsoft.EntityFrameworkCore;

public class RestoreSessionMiddleware
{
    private readonly RequestDelegate _next;

    public RestoreSessionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, CommunityLinkDbContext dbContext)
    {
        if (!context.Session.Keys.Contains("Username") && 
            context.Request.Cookies.TryGetValue("Username", out string username) && 
            context.Request.Cookies.TryGetValue("UserID", out string userIdString) && 
            int.TryParse(userIdString, out int userId))
        {
            var user = await dbContext.Users.Include(u => u.Employee).FirstOrDefaultAsync(u => u.UserID == userId);
            if (user != null)
            {
                context.Session.SetString("Username", user.Username);
                context.Session.SetInt32("UserID", user.UserID);
                context.Session.SetString("IsEmployee", user.Employee != null ? "true" : "false");
                context.Session.SetString("IsAdmin", user.Employee != null && user.Employee.Role == "Admin" ? "true" : "false");
            }
        }

        await _next(context);
    }
}
