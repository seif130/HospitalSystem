using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.FinanceAndInsurance.Refund
{
    public sealed record RefundProcessedDomainEvent(RefundId RefundId, PaymentId OriginalPaymentId, Money Amount) : DomainEvent;

}
