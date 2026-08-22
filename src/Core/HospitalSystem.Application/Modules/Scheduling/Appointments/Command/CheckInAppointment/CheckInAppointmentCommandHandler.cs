using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.CheckInAppointment
{
    public sealed class CheckInAppointmentCommandHandler
      : ICommandHandler<CheckInAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointments;

        public CheckInAppointmentCommandHandler(
            IAppointmentRepository appointments)
        {
            _appointments = appointments;
        }

        public async Task<Result> Handle(CheckInAppointmentCommand request,
            CancellationToken cancellationToken)
        {
            var appointment =
                await _appointments.GetByIdAsync(new AppointmentId(request.AppointmentId),
                    cancellationToken);

            if (appointment is null)
            {
                return Result.Failure(Error.NotFound("Appointment.NotFound",
                        "Appointment was not found."));
            }

            appointment.CheckIn();

            return Result.Success();
        }
    }

}
