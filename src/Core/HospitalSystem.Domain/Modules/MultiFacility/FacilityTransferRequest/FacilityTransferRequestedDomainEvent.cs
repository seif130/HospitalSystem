using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.MultiFacility.FacilityTransferRequest
{
    public sealed record FacilityTransferRequestedDomainEvent(FacilityTransferRequestId RequestId, PatientId PatientId, FacilityId FromFacilityId, FacilityId ToFacilityId) : DomainEvent;

}
