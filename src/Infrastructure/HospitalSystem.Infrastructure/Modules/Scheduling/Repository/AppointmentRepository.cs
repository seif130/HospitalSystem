using HospitalSystem.Domain.Modules.Scheduling.Appointments;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using Microsoft.EntityFrameworkCore;
using Modules.ClinicalAndInpatient.Infrastructure.Persistence.Modules.Scheduling.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.Scheduling.Repository
{
    internal sealed class AppointmentRepository : IAppointmentRepository
    {
        private readonly SchedulingDbContext _context;

        public AppointmentRepository(SchedulingDbContext context) => _context = context;

        public async Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<List<Appointment>> GetByDoctorAndDateAsync(Guid doctorId, DateTime date, CancellationToken cancellationToken = default)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.StartsAt >= startDate && a.StartsAt < endDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Appointment>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
        {
            return await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .OrderByDescending(a => a.StartsAt)
                .ToListAsync(cancellationToken);
        }

        public void Add(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
        }
    }
}
