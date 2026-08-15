using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.AdministrationHrPayroll.Staff
{
    public sealed record StaffTerminatedDomainEvent(StaffId StaffId) : DomainEvent;

}
