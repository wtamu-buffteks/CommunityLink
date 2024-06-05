using Microsoft.EntityFrameworkCore;

namespace TestClassTest.Models {
    public static class SeedData {
        public static void Initialize(IServiceProvider serviceProvider) {
            using (var context = new TestDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<TestDbContext>>())) {
                    if (context.TestClasses.Any()) {
                        return;
                    }

                    context.TestClasses.AddRange(
                        new TestClass{
                            TestName = "Orange"
                        },
                        new TestClass{
                            TestName = "Two"
                        },
                        new TestClass{
                            TestName = "Human"
                        },
                        new TestClass{
                            TestName = "Sun"
                        },
                        new TestClass{
                            TestName = "Water"
                        }
                    );
                    context.SaveChanges();
                }
        }
    }
}