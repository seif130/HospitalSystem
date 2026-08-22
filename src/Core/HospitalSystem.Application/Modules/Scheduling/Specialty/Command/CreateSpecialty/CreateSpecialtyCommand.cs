using HospitalSystem.Application.Modules.Scheduling.Specialty.Dto;
using HospitalSystem.Application.Shared.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Command.CreateSpecialty
{
    public sealed record CreateSpecialtyCommand(
          string Name,
          string? Description = null)
          : ICommand<Guid>;
}
