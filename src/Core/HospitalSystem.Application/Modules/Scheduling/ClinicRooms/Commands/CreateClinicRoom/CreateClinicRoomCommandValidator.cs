using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.CreateClinicRoom
{
    public sealed class CreateClinicRoomCommandValidator
      : AbstractValidator<CreateClinicRoomCommand>
    {
        public CreateClinicRoomCommandValidator()
        {
            RuleFor(x => x.RoomNumber)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.DepartmentId)
                .NotEmpty();

            RuleFor(x => x.Capacity)
                .GreaterThan(0);
        }
    }

}
