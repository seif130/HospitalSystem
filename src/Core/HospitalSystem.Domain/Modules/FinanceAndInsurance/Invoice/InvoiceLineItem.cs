using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.FinanceAndInsurance.Invoice
{
    public sealed record InvoiceLineItem(string Description, Money UnitPrice, int Quantity)
    {
        public Money LineTotal => UnitPrice.Multiply(Quantity);
    }
}
