using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.GetDoctorSchedule
{
    public sealed class GetDoctorScheduleQueryValidator : AbstractValidator<GetDoctorScheduleQuery>
    {
        public GetDoctorScheduleQueryValidator()
        {
            RuleFor(q => q.DoctorId).NotEmpty();
            RuleFor(q => q.Date).NotEmpty();
        }
    }
}
