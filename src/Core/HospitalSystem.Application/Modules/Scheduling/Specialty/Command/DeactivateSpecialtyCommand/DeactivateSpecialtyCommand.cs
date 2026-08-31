using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Command.DeleteSpecialty
{
    public sealed record DeactivateSpecialtyCommand(
           Guid SpecialtyId)
           : ICommand;
}
