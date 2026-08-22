using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Query.GetDoctorSchedule
{
    public sealed class GetDoctorAppointmentsQueryValidator
     : AbstractValidator<GetDoctorAppointmentsQuery>
    {
        public GetDoctorAppointmentsQueryValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty();

            RuleFor(x => x.FromUtc)
                .LessThan(x => x.ToUtc);

            RuleFor(x => x.ToUtc)
                .GreaterThan(x => x.FromUtc);
        }
    }

}
