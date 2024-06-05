using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestClassTest.Models;

namespace CharManager.Pages;

public class IndexModel : PageModel
{
    private readonly TestDbContext _context;
    private readonly ILogger<IndexModel> _logger;
    public List<TestClass> TestClasses { get; set; } = default!;

    public IndexModel(TestDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void OnGet()
    {
        TestClasses = _context.TestClasses.ToList();
    }
}
