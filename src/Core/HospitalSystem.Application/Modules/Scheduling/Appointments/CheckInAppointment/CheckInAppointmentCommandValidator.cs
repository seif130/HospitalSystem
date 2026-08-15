using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.CheckInAppointment
{
    public sealed class CheckInAppointmentCommandValidator : AbstractValidator<CheckInAppointmentCommand>
    {
        public CheckInAppointmentCommandValidator()
        {
            RuleFor(c => c.AppointmentId).NotEmpty();
        }
    }
}
