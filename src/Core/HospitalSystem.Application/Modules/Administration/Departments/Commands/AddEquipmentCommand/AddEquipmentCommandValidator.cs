using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.AddEquipmentCommand
{
    public class AddEquipmentCommandValidator : AbstractValidator<AddEquipmentCommand>
    {
        public AddEquipmentCommandValidator()
        {
            RuleFor(x => x.DepartmentId).NotEmpty();
            RuleFor(x => x.EquipmentName).NotEmpty().MaximumLength(150);
            RuleFor(x => x.SerialNumber).NotEmpty().MaximumLength(100);
            RuleFor(x => x.PurchaseDate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Purchase date cannot be in the future.");
        }
    }
    }
