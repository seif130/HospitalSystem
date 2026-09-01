using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.FinanceAndInsurance.Payment
{
    public sealed record PaymentRecordedDomainEvent(PaymentId PaymentId, InvoiceId InvoiceId, Money Amount) : DomainEvent;

}
