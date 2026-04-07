using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppointmentDbContext _context;

        public AppointmentRepository(AppointmentDbContext context)
        {
            _context = context;
        }

        public List<Appointment> GetAll() => _context.Appointments.ToList();

        public Appointment GetById(int id) => _context.Appointments.Find(id);

        public void Add(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public void Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var appt = _context.Appointments.Find(id);
            if (appt != null)
            {
                _context.Appointments.Remove(appt);
                _context.SaveChanges();
            }
        }
        public List<Appointment> Search(string searchTerm, DateTime? date, AppointmentStatus? status)
{
    var query = _context.Appointments.AsQueryable();

    if (!string.IsNullOrEmpty(searchTerm))
    {
        query = query.Where(a => a.Name.Contains(searchTerm) || a.Email.Contains(searchTerm));
    }

    if (date.HasValue)
    {
        query = query.Where(a => a.Date.Date == date.Value.Date);
    }

    if (status.HasValue)
    {
        query = query.Where(a => a.Status == status.Value);
    }

    return query.ToList();
}
public bool IsSlotBooked(DateTime date)
{
    return _context.Appointments.Any(a => a.Date == date);
}
    }
}