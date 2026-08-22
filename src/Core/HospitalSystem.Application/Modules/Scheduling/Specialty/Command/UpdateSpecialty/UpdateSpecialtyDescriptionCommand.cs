using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Command.UpdateSpecialty
{
    public sealed record UpdateSpecialtyDescriptionCommand(
          Guid SpecialtyId,
          string? Description)
          : ICommand;
}
