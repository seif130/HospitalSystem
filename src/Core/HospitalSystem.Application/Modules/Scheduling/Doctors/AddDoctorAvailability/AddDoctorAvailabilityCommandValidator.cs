using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.AddDoctorAvailability
{
    public sealed class AddDoctorAvailabilityCommandValidator : AbstractValidator<AddDoctorAvailabilityCommand>
    {
        public AddDoctorAvailabilityCommandValidator()
        {
            RuleFor(c => c.DoctorId).NotEmpty();
            RuleFor(c => c.StartUtc).GreaterThan(DateTime.UtcNow);
            RuleFor(c => c.EndUtc).GreaterThan(c => c.StartUtc);
        }
    }
}
