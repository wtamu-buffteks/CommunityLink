using CommunityLink.Models;
using Microsoft.EntityFrameworkCore;

namespace Apart.Models {
    public static class SeedData {
        public static void Initialize(IServiceProvider serviceProvider) {
            using (var context = new CommunityLinkDbContext( 
                serviceProvider.GetRequiredService<DbContextOptions<CommunityLinkDbContext>>())) {
                    if (context.Users.Any()) {
                        return; // No need to seed database
                    }
                    context.AddRange();
                    context.SaveChanges();
                }
        }
    }
}