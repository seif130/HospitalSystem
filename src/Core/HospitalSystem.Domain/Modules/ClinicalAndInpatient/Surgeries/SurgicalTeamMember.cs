using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Clinic.Surgeries.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.Surgeries
{
    public sealed record SurgicalTeamMember(StaffId StaffId, SurgicalRole Role);
}
