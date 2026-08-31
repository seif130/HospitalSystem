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
        Guid SpecialtyId,
        Guid DepartmentId);

}
