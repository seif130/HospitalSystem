using HospitalSystem.Application.Shared.Abstractions;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.MarkAppointmentAsNoShow
{
    public sealed class MarkAppointmentAsNoShowCommandHandler: ICommandHandler<MarkAppointmentAsNoShowCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public MarkAppointmentAsNoShowCommandHandler(
            IAppointmentRepository appointmentRepository,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _dateTimeProvider = dateTimeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            MarkAppointmentAsNoShowCommand request,CancellationToken ct)
        {
            var appointment = await _appointmentRepository
                .GetByIdAsync(new AppointmentId(request.AppointmentId),ct);

            if (appointment is null)
            {
                return Result.Failure(
                    Error.NotFound("Appointment.NotFound","Appointment was not found."));
            }

            appointment.MarkAsNoShow(_dateTimeProvider.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }
    }

}
