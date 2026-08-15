using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.AdministrationHrPayroll.RoomBed
{
    public sealed record RoomBedReservedDomainEvent(RoomBedId RoomBedId, PatientId PatientId) : DomainEvent;

}
