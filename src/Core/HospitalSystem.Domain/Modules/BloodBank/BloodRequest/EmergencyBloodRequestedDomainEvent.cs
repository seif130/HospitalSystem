using HospitalSystem.Domain.Enums;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.BloodBank.BloodRequest
{
    public sealed record EmergencyBloodRequestedDomainEvent(BloodRequestId RequestId, PatientId PatientId, BloodType BloodType, int UnitsRequested) : DomainEvent;

}
