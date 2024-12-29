using Microsoft.AspNetCore.Mvc;
using TravelPackagesApp.Models;

namespace TravelPackagesApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TravelPackageController : ControllerBase
{
    private static List<TravelPackage> _packages = new()
    {
        new TravelPackage { Id = 1, Name = "Package A", Amount = 500, IsActive = true },
        new TravelPackage { Id = 2, Name = "Package B", Amount = 700, IsActive = false }
    };

    // GET: api/TravelPackage
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_packages);
    }

    // GET: api/TravelPackage/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var package = _packages.FirstOrDefault(p => p.Id == id);
        return package != null ? Ok(package) : NotFound();
    }

    // POST: api/TravelPackage
    [HttpPost]
    public IActionResult Create([FromBody] TravelPackage package)
    {
        if (package == null || !ModelState.IsValid)
            return BadRequest("Invalid data.");

        package.Id = _packages.Count + 1;
        _packages.Add(package);
        return CreatedAtAction(nameof(GetById), new { id = package.Id }, package);
    }

    // PUT: api/TravelPackage/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] TravelPackage updatedPackage)
    {
        var package = _packages.FirstOrDefault(p => p.Id == id);
        if (package == null)
            return NotFound();

        package.Name = updatedPackage.Name;
        package.Amount = updatedPackage.Amount;
        package.IsActive = updatedPackage.IsActive;

        return NoContent();
    }

    // DELETE: api/TravelPackage/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var package = _packages.FirstOrDefault(p => p.Id == id);
        if (package == null)
            return NotFound();

        _packages.Remove(package);
        return NoContent();
    }
}