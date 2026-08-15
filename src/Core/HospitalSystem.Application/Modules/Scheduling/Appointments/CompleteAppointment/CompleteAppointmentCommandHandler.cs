using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.CompleteAppointment
{
    public sealed class CompleteAppointmentCommandHandler : ICommandHandler<CompleteAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointments;
        public CompleteAppointmentCommandHandler(IAppointmentRepository appointments) => _appointments = appointments;

        public async Task<Result> Handle(CompleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointments.GetByIdAsync(new AppointmentId(request.AppointmentId), cancellationToken);
            if (appointment is null) return Result.Failure(Error.NotFound("Appointment.NotFound", "Appointment not found."));

            try { appointment.Complete(); }
            catch (DomainException ex) { return Result.Failure(Error.Conflict("Appointment.CompleteFailed", ex.Message)); }

            return Result.Success();
        }
    }
}
