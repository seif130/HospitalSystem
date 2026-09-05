using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Dtos
{
    public sealed record VendorContractDto(
        Guid Id, Guid VendorId, VendorServiceCategory Category,
        DateTime Start, DateTime End, decimal ContractValue,
        string Currency, VendorContractStatus Status);

}
