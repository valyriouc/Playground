using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace NoteAppTesting.Pages;

public class Customer
{
    public int Id { get; set; }

    [Required, StringLength(10)]
    public string? Name { get; set; }
}

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        return Page(); 
    }

    [BindProperty]
    public Customer Customer { get; set; }  

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            ViewData["error"] = "Something was wrong with the model!";
            return Page();
        }

        ViewData["success"] = Customer.Name;
        return Page();
    }
}
