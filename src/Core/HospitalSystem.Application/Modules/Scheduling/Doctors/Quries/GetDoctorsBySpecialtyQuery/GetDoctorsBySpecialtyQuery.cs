using HospitalSystem.Application.Modules.Scheduling.Doctors.Dto;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Quries.GetDoctorsBySpecialtyQuery
{
    public sealed record GetDoctorsBySpecialtyQuery(
        MedicalSpecialty Specialty)
        : IQuery<IReadOnlyList<DoctorDto>>;

}
