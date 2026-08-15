using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.ScheduleAppointment
{
    public sealed class ScheduleAppointmentCommandValidator : AbstractValidator<ScheduleAppointmentCommand>
    {
        public ScheduleAppointmentCommandValidator()
        {
            RuleFor(c => c.PatientId).NotEmpty();
            RuleFor(c => c.DoctorId).NotEmpty();
            RuleFor(c => c.ClinicRoomId).NotEmpty();
            RuleFor(c => c.ScheduledAtUtc).GreaterThan(DateTime.UtcNow).WithMessage("Appointment must be scheduled in the future.");
        }
    }
}
