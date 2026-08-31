using HospitalSystem.Application.Modules.Scheduling.DoctorSchedules.DTOs;
using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorSchedules.Queries.GetDoctorSchedules
{
    public sealed record GetDoctorSchedulesQuery(
       Guid DoctorId)
       : IQuery<IReadOnlyList<DoctorScheduleDto>>;
}
