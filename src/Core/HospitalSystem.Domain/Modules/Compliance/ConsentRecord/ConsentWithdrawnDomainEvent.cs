using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Compliance.ConsentRecord.Enum;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Compliance.ConsentRecord
{
    public sealed record ConsentWithdrawnDomainEvent(ConsentRecordId ConsentRecordId, PatientId PatientId, ConsentType Type) : DomainEvent;

}
