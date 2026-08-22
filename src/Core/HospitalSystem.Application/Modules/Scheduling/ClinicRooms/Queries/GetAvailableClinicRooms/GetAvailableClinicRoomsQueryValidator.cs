using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Queries.GetAvailableClinicRooms
{
    public sealed class GetAvailableClinicRoomsQueryValidator
        : AbstractValidator<GetAvailableClinicRoomsQuery>
    {
        public GetAvailableClinicRoomsQueryValidator()
        {
            RuleFor(x => x.DepartmentId)
                .NotEmpty();

            RuleFor(x => x.FromUtc)
                .LessThan(x => x.ToUtc);

            RuleFor(x => x.ToUtc)
                .GreaterThan(x => x.FromUtc);
        }
    }

}
