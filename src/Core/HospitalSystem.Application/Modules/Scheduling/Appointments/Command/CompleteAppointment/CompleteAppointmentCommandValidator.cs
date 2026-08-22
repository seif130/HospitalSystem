using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.CompleteAppointment
{
    public sealed class CompleteAppointmentCommandValidator
     : AbstractValidator<CompleteAppointmentCommand>
    {
        public CompleteAppointmentCommandValidator()
        {
            RuleFor(x => x.AppointmentId)
                .NotEmpty();
        }
    }

}
