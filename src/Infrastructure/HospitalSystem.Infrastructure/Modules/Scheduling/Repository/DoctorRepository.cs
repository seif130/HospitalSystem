using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using HospitalSystem.Infrastructure.Contexts.DbContextsCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.Scheduling.Repository
{
    internal sealed class DoctorRepository : IDoctorRepository
    {
        private readonly SchedulingDbContext _context;

        public DoctorRepository(SchedulingDbContext context)
        {
            _context = context;
        }
        public async Task<Doctor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doctorId = new DoctorId(id); 

            return await _context.Doctors
                .Include(d => d.AvailabilitySlots)
                .FirstOrDefaultAsync(d => d.Id == doctorId, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doctorId = new DoctorId(id); 

            return await _context.Doctors
                .AnyAsync(d => d.Id == doctorId, cancellationToken);
        }

        public void Add(Doctor doctor)
        {
            ArgumentNullException.ThrowIfNull(doctor);
            _context.Doctors.Add(doctor);
        }
    }
}
