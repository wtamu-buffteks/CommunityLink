using Microsoft.EntityFrameworkCore;

namespace TestClassTest.Models {
    public class TestDbContext : DbContext {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) {}
        public DbSet<TestClass> TestClasses {get; set;} = default!;
    }
}