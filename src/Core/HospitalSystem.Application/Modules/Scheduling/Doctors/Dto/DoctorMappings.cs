using HospitalSystem.Domain.Modules.Scheduling.Doctors;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Dto
{
    public static class DoctorMappings
    {
        public static DoctorDto ToDto(this Doctor doctor)
        {
            return new DoctorDto(
                doctor.Id.Value,
                doctor.Name.FirstName,
                doctor.Name.LastName,
                doctor.LicenseNumber,
                doctor.Specialty,
                doctor.DepartmentId.Value);
        }
    }

}
