using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Enums;
using HospitalSystem.Infrastructure.Contexts.DbContextsCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Repositories.Scheduling
{
    public sealed class DoctorRepository: Repository<Doctor, DoctorId>,IDoctorRepository
    {
        public DoctorRepository(SchedulingDbContext context): base(context)
        {
        }

        public Task<bool> ExistsByLicenseNumberAsync(string licenseNumber,CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(licenseNumber))
                return Task.FromResult(false);

            var normalized = licenseNumber.Trim();

            return DbSet.AnyAsync(x => x.LicenseNumber == normalized,ct);
        }

        public async Task<IReadOnlyList<Doctor>> GetByDepartmentAsync(
            DepartmentId departmentId,CancellationToken ct = default)
        {
            return await DbSet
                .AsNoTracking().Where(x => x.DepartmentId == departmentId)
                .OrderBy(x => x.Name.FirstName).ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Doctor>> GetBySpecialtyAsync(
            MedicalSpecialty specialty,CancellationToken ct = default)
        {
            return await DbSet.AsNoTracking()
                .Where(x => x.Specialty == specialty).OrderBy(x => x.Name.FirstName).ToListAsync(ct);
        }
    }

}
