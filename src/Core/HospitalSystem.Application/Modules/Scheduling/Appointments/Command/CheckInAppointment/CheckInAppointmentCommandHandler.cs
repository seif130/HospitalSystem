using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.CheckInAppointment
{
    public sealed class CheckInAppointmentCommandHandler
      : ICommandHandler<CheckInAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointments;
        private readonly IUnitOfWork _unitOfWork;

        public CheckInAppointmentCommandHandler(
            IAppointmentRepository appointments,
            IUnitOfWork unitOfWork)
        {
            _appointments = appointments;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            CheckInAppointmentCommand request,
            CancellationToken cancellationToken)
        {
            var appointment = await _appointments.GetByIdAsync(
                new AppointmentId(request.AppointmentId),
                cancellationToken);

            if (appointment is null)
            {
                return Result.Failure(
                    Error.NotFound("Appointment.NotFound",
                        "Appointment was not found."));
            }

            appointment.CheckIn();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
