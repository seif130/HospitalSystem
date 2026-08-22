using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Dto
{

    public sealed record SpecialtyDto(
        Guid Id,
        string Name);
}
