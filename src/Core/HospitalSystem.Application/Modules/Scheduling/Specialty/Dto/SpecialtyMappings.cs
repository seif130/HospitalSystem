using HospitalSystem.Application.Modules.Scheduling.Specialty.Dto;
using System;
using System.Collections.Generic;
using System.Text;

using SpecialtyEntity = HospitalSystem.Domain.Modules.Scheduling.Specialties.Specialty;

namespace HospitalSystem.Application.Modules.Scheduling.Specialties;

public static class SpecialtyMappings
{
    public static SpecialtyDto ToDto(
        this SpecialtyEntity specialty)
    {
        return new SpecialtyDto(
            specialty.Id.Value,
            specialty.Name,
            specialty.Description,
            specialty.IsActive);
    }
}
