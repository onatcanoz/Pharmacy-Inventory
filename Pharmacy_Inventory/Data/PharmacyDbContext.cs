using Microsoft.EntityFrameworkCore;
using Pharmacy_Inventory.Models.Field;

namespace Pharmacy_Inventory.Data
{
    public class PharmacyDbContext : DbContext
    {
        public PharmacyDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Medicine> Medicines { get; set; }
    }
}
