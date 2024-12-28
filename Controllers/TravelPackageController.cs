using Microsoft.AspNetCore.Mvc;
using TravelPackagesApp.Models;

public class TravelPackageController : Controller
{
    private static List<TravelPackage> _packages = new();

    public IActionResult Index()
    {
        return View(_packages);
    }

    [HttpPost]
    public IActionResult Create([FromBody] TravelPackage package)
    {
        if (ModelState.IsValid)
        {
            package.Id = _packages.Count + 1;
            _packages.Add(package);

            // Return 200 OK for AJAX requests
            return Json(new { success = true });
        }

        // Return 400 Bad Request for invalid data
        return BadRequest(new { error = "Invalid data" });
    }
}