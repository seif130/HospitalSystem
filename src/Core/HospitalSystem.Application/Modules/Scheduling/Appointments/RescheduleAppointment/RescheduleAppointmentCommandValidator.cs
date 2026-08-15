using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.RescheduleAppointment
{
    public sealed class RescheduleAppointmentCommandValidator : AbstractValidator<RescheduleAppointmentCommand>
    {
        public RescheduleAppointmentCommandValidator()
        {
            RuleFor(c => c.AppointmentId).NotEmpty();
            RuleFor(c => c.NewTimeUtc).GreaterThan(DateTime.UtcNow);
        }
    }
}
