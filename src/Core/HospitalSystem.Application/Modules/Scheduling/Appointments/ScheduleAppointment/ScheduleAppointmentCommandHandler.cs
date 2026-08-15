using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.ScheduleAppointment
{
    public sealed class ScheduleAppointmentCommandHandler : ICommandHandler<ScheduleAppointmentCommand, Guid>
    {
        private readonly IAppointmentRepository _appointments;
        private readonly IDoctorRepository _doctors;
        private readonly IClinicRoomRepository _rooms;

        public ScheduleAppointmentCommandHandler(
            IAppointmentRepository appointments,
            IDoctorRepository doctors,
            IClinicRoomRepository rooms)
        {
            _appointments = appointments;
            _doctors = doctors;
            _rooms = rooms;
        }

        public async Task<Result<Guid>> Handle(ScheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            var doctorId = new DoctorId(request.DoctorId);
            var doctor = await _doctors.GetByIdAsync(doctorId, cancellationToken);
            if (doctor is null)
                return Result.Failure<Guid>(Error.NotFound("Doctor.NotFound", $"No doctor found with id '{request.DoctorId}'."));

            if (!doctor.IsAvailable(request.ScheduledAtUtc))
                return Result.Failure<Guid>(Error.Conflict("Doctor.NotAvailable", "Doctor is not available at the requested time."));

            var room = await _rooms.GetByIdAsync(new ClinicRoomId(request.ClinicRoomId), cancellationToken);
            if (room is null)
                return Result.Failure<Guid>(Error.NotFound("ClinicRoom.NotFound", $"No clinic room found with id '{request.ClinicRoomId}'."));

            Appointment appointment;
            try
            {
                appointment = Appointment.Schedule(
                    new PatientId(request.PatientId),
                    doctorId,
                    room.Id,
                    request.ScheduledAtUtc,
                    request.Reason);

                room.Book(DateRange.Create(request.ScheduledAtUtc, request.ScheduledAtUtc.AddMinutes(30)));
            }
            catch (DomainException ex)
            {
                return Result.Failure<Guid>(Error.Conflict("Appointment.CannotSchedule", ex.Message));
            }

            _appointments.Add(appointment);
            return Result.Success(appointment.Id.Value);
        }
    }
}
