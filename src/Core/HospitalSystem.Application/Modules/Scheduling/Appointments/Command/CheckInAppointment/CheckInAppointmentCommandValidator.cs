using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.CheckInAppointment
{
    public sealed class CheckInAppointmentCommandValidator
      : AbstractValidator<CheckInAppointmentCommand>
    {
        public CheckInAppointmentCommandValidator()
        {
            RuleFor(x => x.AppointmentId)
                .NotEmpty();
        }
    }

}
