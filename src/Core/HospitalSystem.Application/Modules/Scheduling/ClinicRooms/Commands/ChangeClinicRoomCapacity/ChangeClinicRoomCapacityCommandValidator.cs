using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Commands.ChangeClinicRoomCapacity
{
    public sealed class ChangeClinicRoomCapacityCommandValidator
        : AbstractValidator<ChangeClinicRoomCapacityCommand>
    {
        public ChangeClinicRoomCapacityCommandValidator()
        {
            RuleFor(x => x.ClinicRoomId)
                .NotEmpty();

            RuleFor(x => x.Capacity)
                .GreaterThan(0);
        }
    }

}
