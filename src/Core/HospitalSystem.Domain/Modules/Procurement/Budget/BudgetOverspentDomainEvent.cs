using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Procurement.Budget
{
    public sealed record BudgetOverspentDomainEvent(BudgetId BudgetId, DepartmentId DepartmentId, Money SpentAmount, Money AllocatedAmount) : DomainEvent;

}
