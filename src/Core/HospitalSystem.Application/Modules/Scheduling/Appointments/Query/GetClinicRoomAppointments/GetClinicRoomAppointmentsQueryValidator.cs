using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Query.GetClinicRoomAppointments
{
    public sealed class GetClinicRoomAppointmentsQueryValidator
        : AbstractValidator<GetClinicRoomAppointmentsQuery>
    {
        public GetClinicRoomAppointmentsQueryValidator()
        {
            RuleFor(x => x.ClinicRoomId)
                .NotEmpty();

            RuleFor(x => x.FromUtc)
                .LessThan(x => x.ToUtc);

            RuleFor(x => x.ToUtc)
                .GreaterThan(x => x.FromUtc);
        }
    }

}
