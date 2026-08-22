using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.RescheduleAppointment
{
    public sealed class RescheduleAppointmentCommandValidator
     : AbstractValidator<RescheduleAppointmentCommand>
    {
        public RescheduleAppointmentCommandValidator()
        {
            RuleFor(x => x.AppointmentId)
                .NotEmpty();

            RuleFor(x => x.StartUtc)
                .LessThan(x => x.EndUtc);

            RuleFor(x => x.EndUtc)
                .GreaterThan(x => x.StartUtc);
        }
    }

}
