using HospitalSystem.Application.Modules.Scheduling.Waitlists.Dto;
using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Quries.GetWaitlistEntryByIdQuery
{
    public sealed record GetWaitlistByIdQuery(
        Guid WaitlistId)
        : IQuery<WaitlistDto>;

}
