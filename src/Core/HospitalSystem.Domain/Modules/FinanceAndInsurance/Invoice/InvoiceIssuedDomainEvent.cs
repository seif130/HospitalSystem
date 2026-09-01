using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.FinanceAndInsurance.Invoice
{
    public sealed record InvoiceIssuedDomainEvent(InvoiceId InvoiceId, PatientId PatientId, Money Total) : DomainEvent;

}
