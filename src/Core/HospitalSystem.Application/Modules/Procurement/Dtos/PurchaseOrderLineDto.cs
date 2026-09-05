using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Dtos
{
    public sealed record PurchaseOrderLineDto(string ItemName, int Quantity, decimal UnitPrice,
        decimal Total, string Currency);

}
