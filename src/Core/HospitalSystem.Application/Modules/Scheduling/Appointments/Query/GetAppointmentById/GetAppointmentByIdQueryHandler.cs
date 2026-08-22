using HospitalSystem.Application.Modules.Scheduling.Appointments.Dto;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Query.GetAppointmentById
{
    public sealed class GetAppointmentByIdQueryHandler
       : IQueryHandler<GetAppointmentByIdQuery, AppointmentDto>
    {
        private readonly IAppointmentRepository _appointments;

        public GetAppointmentByIdQueryHandler(
            IAppointmentRepository appointments)
        {
            _appointments = appointments;
        }

        public async Task<Result<AppointmentDto>> Handle(
            GetAppointmentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var appointment = await _appointments.GetByIdAsync(
                new AppointmentId(request.AppointmentId),
                cancellationToken);

            if (appointment is null)
            {
                return Result.Failure<AppointmentDto>(
                    Error.NotFound(
                        "Appointment.NotFound",
                        "Appointment was not found."));
            }

            return appointment.ToDto();
        }
    }


}
