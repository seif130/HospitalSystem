using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Command.ReactivateSpecialty
{
    public sealed record ReactivateSpecialtyCommand(
           Guid SpecialtyId)
           : ICommand;
}
