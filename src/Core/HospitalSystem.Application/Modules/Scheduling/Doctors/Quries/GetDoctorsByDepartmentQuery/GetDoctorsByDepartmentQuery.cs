using HospitalSystem.Application.Modules.Scheduling.Doctors.Dto;
using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Quries.GetDoctorsByDepartmentQuery
{
    public sealed record GetDoctorsByDepartmentQuery(
       Guid DepartmentId)
       : IQuery<IReadOnlyList<DoctorDto>>;

}
