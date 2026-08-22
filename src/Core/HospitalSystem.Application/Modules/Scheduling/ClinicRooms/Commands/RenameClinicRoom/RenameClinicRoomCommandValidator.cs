using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Commands.RenameClinicRoom
{
    public sealed class RenameClinicRoomCommandValidator
        : AbstractValidator<RenameClinicRoomCommand>
    {
        public RenameClinicRoomCommandValidator()
        {
            RuleFor(x => x.ClinicRoomId)
                .NotEmpty();

            RuleFor(x => x.RoomNumber)
                .NotEmpty()
                .MaximumLength(50);
        }
    }

}
