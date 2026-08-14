using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Procurement.Budget
{
    public sealed record BudgetExpenseLine(string Description, Money Amount, DateTime IncurredOnUtc);
}
