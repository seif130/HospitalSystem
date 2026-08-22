using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Commands.ChangeClinicRoomDepartment
{
    public sealed class ChangeClinicRoomDepartmentCommandValidator
        : AbstractValidator<ChangeClinicRoomDepartmentCommand>
    {
        public ChangeClinicRoomDepartmentCommandValidator()
        {
            RuleFor(x => x.ClinicRoomId)
                .NotEmpty();

            RuleFor(x => x.DepartmentId)
                .NotEmpty();
        }
    }

}
