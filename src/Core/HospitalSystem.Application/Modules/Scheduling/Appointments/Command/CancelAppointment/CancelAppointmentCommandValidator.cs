using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.CancelAppointment
{
    public sealed class CancelAppointmentCommandValidator
       : AbstractValidator<CancelAppointmentCommand>
    {
        public CancelAppointmentCommandValidator()
        {
            RuleFor(x => x.AppointmentId)
                .NotEmpty();

            RuleFor(x => x.Reason)
                .NotEmpty()
                .MaximumLength(500);
        }
    }

}
