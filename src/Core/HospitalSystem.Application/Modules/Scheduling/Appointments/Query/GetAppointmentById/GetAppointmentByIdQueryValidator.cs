using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Query.GetAppointmentById
{
    public sealed class GetAppointmentByIdQueryValidator
        : AbstractValidator<GetAppointmentByIdQuery>
    {
        public GetAppointmentByIdQueryValidator()
        {
            RuleFor(x => x.AppointmentId)
                .NotEmpty();
        }
    }

}
