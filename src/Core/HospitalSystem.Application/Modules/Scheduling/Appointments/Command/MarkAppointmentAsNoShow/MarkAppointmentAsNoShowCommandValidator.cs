using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.MarkAppointmentAsNoShow
{
    public sealed class MarkAppointmentAsNoShowCommandValidator
        : AbstractValidator<MarkAppointmentAsNoShowCommand>
    {
        public MarkAppointmentAsNoShowCommandValidator()
        {
            RuleFor(x => x.AppointmentId)
                .NotEmpty();
        }
    }

}
