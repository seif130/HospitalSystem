using HospitalSystem.Application.Modules.Scheduling.Waitlists.Dto;
using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Quries.GetWaitingByDoctorQuery
{
    public sealed record GetWaitingByDoctorQuery(
       Guid DoctorId,
       DateTime FromUtc,
       DateTime ToUtc)
       : IQuery<IReadOnlyList<WaitlistDto>>;

}
