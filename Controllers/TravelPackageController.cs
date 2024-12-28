using Microsoft.AspNetCore.Mvc;
using TravelPackagesApp.Models;

namespace TravelPackagesApp.Controllers;
public class TravelPackageController : Controller
{
    private static List<TravelPackage> _packages = new();

    public IActionResult Index()
    {
        return View(_packages);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(TravelPackage package)
    {
        if (ModelState.IsValid)
        {
            package.Id = _packages.Count + 1;
            _packages.Add(package);
            return RedirectToAction("Index");
        }
        return View(package);
    }
}