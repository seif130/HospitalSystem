using HospitalSystem.Application.Modules.Scheduling.DoctorSchedules.DTOs;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorSchedules.Queries.GetDoctorSchedules
{
    public sealed class GetDoctorSchedulesQueryHandler
        : IQueryHandler<
            GetDoctorSchedulesQuery,
            IReadOnlyList<DoctorScheduleDto>>
    {
        private readonly IDoctorScheduleRepository _schedules;

        public GetDoctorSchedulesQueryHandler(
            IDoctorScheduleRepository schedules)
        {
            _schedules = schedules;
        }

        public async Task<Result<IReadOnlyList<DoctorScheduleDto>>> Handle(
            GetDoctorSchedulesQuery request,
            CancellationToken cancellationToken)
        {
            var schedules = await _schedules.GetByDoctorAsync(
                new DoctorId(request.DoctorId),
                cancellationToken);

            return schedules
                .Select(x => x.ToDto())
                .ToList();
        }
    }
}
