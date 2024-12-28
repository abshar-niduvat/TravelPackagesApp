namespace TravelPackagesApp.Models;

public class TravelPackage
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
    }
