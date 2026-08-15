using HospitalSystem.Application.Modules.Scheduling.Appointments.GetDoctorSchedule;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.GetPatientAppointments
{
    public sealed class GetPatientAppointmentsQueryHandler : IQueryHandler<GetPatientAppointmentsQuery, IReadOnlyList<AppointmentDto>>
    {
        private readonly IAppointmentRepository _appointments;

        public GetPatientAppointmentsQueryHandler(IAppointmentRepository appointments) => _appointments = appointments;

        public async Task<Result<IReadOnlyList<AppointmentDto>>> Handle(GetPatientAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var patientId = new PatientId(request.PatientId);
            var appointments = await _appointments.GetByPatientIdAsync(patientId, cancellationToken);
            return Result.Success<IReadOnlyList<AppointmentDto>>(appointments.Select(a => a.ToDto()).ToList());
        }
    }
}
