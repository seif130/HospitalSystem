using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Command.ChangeDoctorLicenseNumberCommand
{
    public sealed class ChangeDoctorLicenseNumberCommandValidator
        : AbstractValidator<ChangeDoctorLicenseNumberCommand>
    {
        public ChangeDoctorLicenseNumberCommandValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty();

            RuleFor(x => x.LicenseNumber)
                .NotEmpty()
                .MaximumLength(100);
        }
    }

}
