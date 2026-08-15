using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.CancelAppointment
{
    public sealed class CancelAppointmentCommandValidator : AbstractValidator<CancelAppointmentCommand>
    {
        public CancelAppointmentCommandValidator()
        {
            RuleFor(c => c.AppointmentId).NotEmpty();
            RuleFor(c => c.Reason).NotEmpty().MaximumLength(500);
        }
    }
}
