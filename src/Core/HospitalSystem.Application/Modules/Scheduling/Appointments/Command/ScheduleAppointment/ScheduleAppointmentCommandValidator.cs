using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.ScheduleAppointment
{
    public sealed class ScheduleAppointmentCommandValidator
     : AbstractValidator<ScheduleAppointmentCommand>
    {
        public ScheduleAppointmentCommandValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty();

            RuleFor(x => x.DoctorId)
                .NotEmpty();

            RuleFor(x => x.ClinicRoomId)
                .NotEmpty();

            RuleFor(x => x.StartUtc)
                .LessThan(x => x.EndUtc);

            RuleFor(x => x.EndUtc)
                .GreaterThan(x => x.StartUtc);

            RuleFor(x => x.Type)
                .IsInEnum();

            RuleFor(x => x.Reason)
                .MaximumLength(500);
        }
    }

}
