using HospitalSystem.Application.Shared.Abstractions;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.CancelAppointment
{
    public sealed class CancelAppointmentCommandHandler: ICommandHandler<CancelAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointments;

        public CancelAppointmentCommandHandler(
            IAppointmentRepository appointments)
        {
            _appointments = appointments;
        }

        public async Task<Result> Handle(
            CancelAppointmentCommand request,
            CancellationToken cancellationToken)
        {
            var appointment =
                await _appointments.GetByIdAsync(
                    new AppointmentId(request.AppointmentId),
                    cancellationToken);

            if (appointment is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "Appointment.NotFound",
                        "Appointment was not found."));
            }

            appointment.Cancel(request.Reason);

            return Result.Success();
        }
    }

}
