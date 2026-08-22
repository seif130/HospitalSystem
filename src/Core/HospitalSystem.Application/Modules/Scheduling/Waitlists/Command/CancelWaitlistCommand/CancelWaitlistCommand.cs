using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Command.CancelWaitlistCommand
{
    public sealed record CancelWaitlistCommand(
        Guid WaitlistId)
        : ICommand;

}
