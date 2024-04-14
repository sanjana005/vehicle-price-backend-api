using Microsoft.EntityFrameworkCore;
using vehicle_price_backend_api.Models;

namespace vehicle_price_backend_api.Data
{
    public class UserAPIDbCon : DbContext
    {
        public UserAPIDbCon(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
