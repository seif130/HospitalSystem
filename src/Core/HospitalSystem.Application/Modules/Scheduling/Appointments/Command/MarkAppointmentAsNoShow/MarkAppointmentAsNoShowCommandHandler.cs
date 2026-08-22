using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.MarkAppointmentAsNoShow
{
    public sealed class MarkAppointmentAsNoShowCommandHandler
        : ICommandHandler<MarkAppointmentAsNoShowCommand>
    {
        private readonly IAppointmentRepository _appointments;

        public MarkAppointmentAsNoShowCommandHandler(
            IAppointmentRepository appointments)
        {
            _appointments = appointments;
        }

        public async Task<Result> Handle(
            MarkAppointmentAsNoShowCommand request,
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

            appointment.MarkAsNoShow();

            return Result.Success();
        }
    }

}
