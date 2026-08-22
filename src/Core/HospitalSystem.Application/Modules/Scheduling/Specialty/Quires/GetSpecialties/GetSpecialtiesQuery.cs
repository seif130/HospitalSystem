using HospitalSystem.Application.Modules.Scheduling.Specialty.Dto;
using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Quires.GetSpecialties
{
    public sealed record GetSpecialtiesQuery
        : IQuery<IReadOnlyList<SpecialtyDto>>;
}
