using HospitalSystem.Application.Modules.Scheduling.Specialty.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Command.CreateSpecialty
{

    public sealed record CreateSpecialtyCommand(
        string Name
    ) : IRequest<SpecialtyDto>;
}
