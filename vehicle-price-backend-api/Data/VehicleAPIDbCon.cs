using Microsoft.EntityFrameworkCore;
using vehicle_price_backend_api.Models;

namespace vehicle_price_backend_api.Data
{
    public class VehicleAPIDbCon : DbContext
    {
        public VehicleAPIDbCon(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
