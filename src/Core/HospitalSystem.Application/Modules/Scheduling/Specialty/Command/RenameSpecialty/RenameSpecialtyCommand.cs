using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Command.RenameSpecialty
{
    public sealed record RenameSpecialtyCommand(
            Guid SpecialtyId,
            string Name)
            : ICommand;
}
