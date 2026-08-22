using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Command.CreateDoctorCommand
{
    public sealed class CreateDoctorCommandValidator
        : AbstractValidator<CreateDoctorCommand>
    {
        public CreateDoctorCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.DepartmentId)
                .NotEmpty();

            RuleFor(x => x.LicenseNumber)
                .NotEmpty()
                .MaximumLength(100);
        }
    }

}
