using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorsTimeOff.DTOs
{
    public sealed record DoctorTimeOffDto(
        Guid Id,
        Guid DoctorId,
        DateTime StartUtc,
        DateTime? EndUtc,
        string? Reason);
}
