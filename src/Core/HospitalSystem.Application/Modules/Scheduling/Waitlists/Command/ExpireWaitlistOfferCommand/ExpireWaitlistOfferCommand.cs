using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Command.ExpireWaitlistOfferCommand
{
    public sealed record ExpireWaitlistOfferCommand(
        Guid WaitlistId) : ICommand;
}
