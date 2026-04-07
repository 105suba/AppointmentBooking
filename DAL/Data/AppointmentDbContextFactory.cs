using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DAL.Data
{
    public class AppointmentDbContextFactory : IDesignTimeDbContextFactory<AppointmentDbContext>
    {
        public AppointmentDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppointmentDbContext>();
            optionsBuilder.UseSqlite("Data Source=appointments.db"); // same as in Program.cs

            return new AppointmentDbContext(optionsBuilder.Options);
        }
    }
}