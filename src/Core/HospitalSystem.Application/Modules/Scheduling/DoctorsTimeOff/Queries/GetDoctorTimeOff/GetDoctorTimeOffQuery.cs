using HospitalSystem.Application.Modules.Scheduling.DoctorsTimeOff.DTOs;
using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorsTimeOff.Queries.GetDoctorTimeOff
{
    public sealed record GetDoctorTimeOffQuery(
        Guid DoctorId)
        : IQuery<IReadOnlyList<DoctorTimeOffDto>>;
}
