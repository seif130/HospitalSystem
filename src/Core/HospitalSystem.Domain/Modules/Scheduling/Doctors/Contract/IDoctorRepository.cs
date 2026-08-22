using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Enums;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract
{
    public interface IDoctorRepository: IRepository<Doctor, DoctorId>
    {
        Task<bool> ExistsByLicenseNumberAsync(string licenseNumber, CancellationToken ct = default);

        Task<IReadOnlyList<Doctor>> GetByDepartmentAsync(DepartmentId departmentId,  CancellationToken ct = default);

        Task<IReadOnlyList<Doctor>> GetBySpecialtyAsync(MedicalSpecialty specialty,CancellationToken ct = default);
    }


}
