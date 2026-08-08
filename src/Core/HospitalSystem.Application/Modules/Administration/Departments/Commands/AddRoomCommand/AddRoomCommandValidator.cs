using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.AddRoomCommand
{
    public sealed class AddRoomCommandValidator : AbstractValidator<AddRoomCommand>
    {
        public AddRoomCommandValidator()
        {
            RuleFor(x => x.DepartmentId).NotEmpty().WithMessage("Department ID is required.");
            RuleFor(x => x.RoomNumber).NotEmpty().MaximumLength(50).WithMessage("Room number is required.");
            RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid room type.");
        }
    }
}
