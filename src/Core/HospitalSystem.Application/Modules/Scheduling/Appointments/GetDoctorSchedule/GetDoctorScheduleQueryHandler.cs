using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.GetDoctorSchedule
{
    public sealed class GetDoctorScheduleQueryHandler : IQueryHandler<GetDoctorScheduleQuery, IReadOnlyList<AppointmentDto>>
    {
        private readonly IAppointmentRepository _appointments;
        public GetDoctorScheduleQueryHandler(IAppointmentRepository appointments) => _appointments = appointments;

        public async Task<Result<IReadOnlyList<AppointmentDto>>> Handle(GetDoctorScheduleQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _appointments.GetByDoctorAndDateAsync(new DoctorId(request.DoctorId), request.Date, cancellationToken);
            return Result.Success<IReadOnlyList<AppointmentDto>>(appointments.Select(a => a.ToDto()).ToList());
        }
    }
}
