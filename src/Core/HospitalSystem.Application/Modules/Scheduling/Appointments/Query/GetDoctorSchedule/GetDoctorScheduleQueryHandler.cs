using HospitalSystem.Application.Modules.Scheduling.Appointments.Dto;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Contract;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Query.GetDoctorSchedule
{
    public sealed class GetDoctorAppointmentsQueryHandler
     : IQueryHandler<GetDoctorAppointmentsQuery,IReadOnlyList<AppointmentDto>>
    {
        private readonly IAppointmentRepository _appointments;

        public GetDoctorAppointmentsQueryHandler(
            IAppointmentRepository appointments)
        {
            _appointments = appointments;
        }

        public async Task<Result<IReadOnlyList<AppointmentDto>>> Handle(
            GetDoctorAppointmentsQuery request,
            CancellationToken cancellationToken)
        {
            var period = DateRange.Create(
                request.FromUtc,
                request.ToUtc);

            var appointments = await _appointments.GetByDoctorAsync(
                new DoctorId(request.DoctorId),
                period,
                cancellationToken);

            var result = appointments
                .Select(x => x.ToDto())
                .ToList();

            return result;
        }
    }


}
