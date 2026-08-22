using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Contract;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.RescheduleAppointment
{
    public sealed class RescheduleAppointmentCommandHandler
       : ICommandHandler<RescheduleAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointments;

        public RescheduleAppointmentCommandHandler(
            IAppointmentRepository appointments)
        {
            _appointments = appointments;
        }

        public async Task<Result> Handle(
            RescheduleAppointmentCommand request,
            CancellationToken cancellationToken)
        {
            var appointmentId =
                new AppointmentId(request.AppointmentId);

            var appointment =
                await _appointments.GetByIdAsync(
                    appointmentId,
                    cancellationToken);

            if (appointment is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "Appointment.NotFound",
                        "Appointment was not found."));
            }

            var newPeriod = DateRange.Create(
                request.StartUtc,
                request.EndUtc);

            if (await _appointments.HasDoctorConflictAsync(
                    appointment.DoctorId,
                    newPeriod,
                    appointment.Id,
                    cancellationToken))
            {
                return Result.Failure(
                    Error.Conflict(
                        "Appointment.DoctorConflict",
                        "Doctor is already booked during this period."));
            }

            if (await _appointments.HasPatientConflictAsync(
                    appointment.PatientId,
                    newPeriod,
                    appointment.Id,
                    cancellationToken))
            {
                return Result.Failure(
                    Error.Conflict(
                        "Appointment.PatientConflict",
                        "Patient already has an appointment during this period."));
            }

            if (await _appointments.HasClinicRoomConflictAsync(
                    appointment.ClinicRoomId,
                    newPeriod,
                    appointment.Id,
                    cancellationToken))
            {
                return Result.Failure(
                    Error.Conflict(
                        "Appointment.ClinicRoomConflict",
                        "Clinic room is already booked during this period."));
            }

            appointment.Reschedule(newPeriod);

            return Result.Success();
        }
    }

}
