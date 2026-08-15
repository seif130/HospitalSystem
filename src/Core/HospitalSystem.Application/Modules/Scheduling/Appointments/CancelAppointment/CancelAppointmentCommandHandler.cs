using HospitalSystem.Application.Shared.Abstractions;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.CancelAppointment
{
    public sealed class CancelAppointmentCommandHandler : ICommandHandler<CancelAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointments;
        private readonly IUnitOfWork _unitOfWork;

        public CancelAppointmentCommandHandler(
            IAppointmentRepository appointments,
            IUnitOfWork unitOfWork)
        {
            _appointments = appointments;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {

            var appointmentId = new AppointmentId(request.AppointmentId);
            var appointment = await _appointments.GetByIdAsync(appointmentId, cancellationToken);

            if (appointment is null)
            {
                return Result.Failure(Error.NotFound("Appointment.NotFound", "Appointment not found."));
            }


            try
            {
                appointment.Cancel(request.Reason);
            }
            catch (DomainException ex)
            {
                return Result.Failure(Error.Conflict("Appointment.CannotCancel", ex.Message));
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
