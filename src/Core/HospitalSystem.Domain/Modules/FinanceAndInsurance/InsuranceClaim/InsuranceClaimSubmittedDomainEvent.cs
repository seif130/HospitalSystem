using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.FinanceAndInsurance.InsuranceClaim
{
    public sealed record InsuranceClaimSubmittedDomainEvent(InsuranceClaimId ClaimId, InvoiceId InvoiceId, Money ClaimedAmount) : DomainEvent;

}
