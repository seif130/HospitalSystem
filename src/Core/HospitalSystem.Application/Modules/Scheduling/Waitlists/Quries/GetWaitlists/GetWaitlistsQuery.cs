using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Scheduling.Waitlists.Dto;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Quries.GetWaitlists
{
    public sealed record GetWaitlistsQuery(
        Guid? DoctorId,
        Guid? PatientId,
        WaitlistEntryStatus? Status,
        int Page = 1,
        int PageSize = 20)
        : IQuery<PaginatedList<WaitlistDto>>;
}
