using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}