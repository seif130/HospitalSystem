using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.CreateClinicRoom
{
    public sealed class CreateClinicRoomCommandValidator : AbstractValidator<CreateClinicRoomCommand>
    {
        public CreateClinicRoomCommandValidator()
        {
            RuleFor(c => c.RoomNumber).NotEmpty().MaximumLength(50);
            RuleFor(c => c.Description).MaximumLength(250);
        }
    }
}
