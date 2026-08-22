using HospitalSystem.Domain.Modules.Scheduling.Doctors.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Dto
{
    public sealed record DoctorDto(
        Guid Id,
        string FirstName,
        string LastName,
        string LicenseNumber,
        MedicalSpecialty Specialty,
        Guid DepartmentId);

}
