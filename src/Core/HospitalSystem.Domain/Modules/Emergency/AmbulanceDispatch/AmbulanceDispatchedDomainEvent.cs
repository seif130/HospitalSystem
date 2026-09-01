using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Emergency.AmbulanceDispatch
{
    public sealed record AmbulanceDispatchedDomainEvent(AmbulanceDispatchId DispatchId, AmbulanceId AmbulanceId, string PickupAddress) : DomainEvent;

}
