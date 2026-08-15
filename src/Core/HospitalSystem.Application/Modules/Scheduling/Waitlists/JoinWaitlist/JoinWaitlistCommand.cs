using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.JoinWaitlist
{
    public sealed record JoinWaitlistCommand(Guid PatientId, Guid DoctorId, DateTime PreferredFromUtc, DateTime PreferredToUtc) : ICommand<Guid>;
}
