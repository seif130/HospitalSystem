using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Dtos
{
    public sealed record PurchaseRequestLineDto(string ItemName, int Quantity, decimal EstimatedUnitPrice,
        decimal EstimatedTotal, string Currency);

}
