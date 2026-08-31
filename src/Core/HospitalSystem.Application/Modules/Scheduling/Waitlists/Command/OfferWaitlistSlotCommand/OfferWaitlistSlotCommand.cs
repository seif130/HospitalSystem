using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Command.OfferWaitlistSlotCommand
{
    public sealed record OfferWaitlistSlotCommand(
        Guid WaitlistId,
        Guid AppointmentId) : ICommand;
}
