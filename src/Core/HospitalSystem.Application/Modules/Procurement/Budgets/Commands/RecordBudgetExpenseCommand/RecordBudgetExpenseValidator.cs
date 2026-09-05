using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Budgets.Commands.RecordBudgetExpenseCommand
{
    public sealed class RecordBudgetExpenseValidator : AbstractValidator<RecordBudgetExpenseCommand> 
    {
        public RecordBudgetExpenseValidator()
        { 
            RuleFor(x => x.BudgetId.Value).NotEmpty(); 
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Amount).GreaterThan(0); RuleFor(x => x.Currency).NotEmpty().Length(3);
        }
    }

}
