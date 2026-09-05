using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Dtos
{
    public sealed record PurchaseRequestDto(Guid Id, Guid DepartmentId, string Reason,
        PurchaseRequestStatus Status, IReadOnlyList<PurchaseRequestLineDto> Lines);

}
