using DAL.Models;
using System.Collections.Generic;

namespace DAL.Repository
{
    public interface IAppointmentRepository
    {
        List<Appointment> GetAll();           // List all appointments
        Appointment GetById(int id);         // Get single appointment
        void Add(Appointment appointment);    // Add new appointment
        void Update(Appointment appointment); // Update existing appointment
        void Delete(int id);                  // Delete appointment
        List<Appointment> Search(string searchTerm, DateTime? date, AppointmentStatus? status);
        bool IsSlotBooked(DateTime date);
    }
}