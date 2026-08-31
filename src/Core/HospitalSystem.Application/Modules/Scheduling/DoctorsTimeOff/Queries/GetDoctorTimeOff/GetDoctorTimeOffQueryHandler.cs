using HospitalSystem.Application.Modules.Scheduling.DoctorsTimeOff.DTOs;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorsTimeOff.Queries.GetDoctorTimeOff
{
    public sealed class GetDoctorTimeOffQueryHandler
        : IQueryHandler<GetDoctorTimeOffQuery,IReadOnlyList<DoctorTimeOffDto>>
    {
        private readonly IDoctorTimeOffRepository _timeOffs;

        public GetDoctorTimeOffQueryHandler(
            IDoctorTimeOffRepository timeOffs)
        {
            _timeOffs = timeOffs;
        }

        public async Task<Result<IReadOnlyList<DoctorTimeOffDto>>> Handle(
            GetDoctorTimeOffQuery request,
            CancellationToken cancellationToken)
        {
            var timeOffs = await _timeOffs.GetByDoctorAsync(
                new DoctorId(request.DoctorId),
                cancellationToken);

            return timeOffs
                .Select(x => x.ToDto())
                .ToList();
        }
    }
}
