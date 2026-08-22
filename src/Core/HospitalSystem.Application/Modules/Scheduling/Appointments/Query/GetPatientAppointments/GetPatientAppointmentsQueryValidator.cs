using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Query.GetPatientAppointments
{
    public sealed class GetPatientAppointmentsQueryValidator
        : AbstractValidator<GetPatientAppointmentsQuery>
    {
        public GetPatientAppointmentsQueryValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty();

            RuleFor(x => x.FromUtc)
                .LessThan(x => x.ToUtc);

            RuleFor(x => x.ToUtc)
                .GreaterThan(x => x.FromUtc);
        }
    }

}
