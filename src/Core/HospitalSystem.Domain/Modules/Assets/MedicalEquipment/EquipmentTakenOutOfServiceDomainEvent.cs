using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Assets.MedicalEquipment
{
    public sealed record EquipmentTakenOutOfServiceDomainEvent(MedicalEquipmentId EquipmentId, string Reason) : DomainEvent;

}
