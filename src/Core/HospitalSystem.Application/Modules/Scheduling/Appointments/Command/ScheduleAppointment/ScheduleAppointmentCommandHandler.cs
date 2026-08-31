using HospitalSystem.Application.Shared.Abstractions;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Contract;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.ScheduleAppointment
{
    public sealed class ScheduleAppointmentCommandHandler
      : ICommandHandler<ScheduleAppointmentCommand, Guid>
    {
        private readonly IAppointmentRepository _appointments;
        private readonly IDoctorRepository _doctors;
        private readonly IClinicRoomRepository _clinicRooms;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public ScheduleAppointmentCommandHandler(
            IAppointmentRepository appointments,
            IDoctorRepository doctors,
            IClinicRoomRepository clinicRooms,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork)
        {
            _appointments = appointments;
            _doctors = doctors;
            _clinicRooms = clinicRooms;
            _dateTimeProvider = dateTimeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            ScheduleAppointmentCommand request,
            CancellationToken cancellationToken)
        {
            var doctorId = new DoctorId(request.DoctorId);
            var clinicRoomId = new ClinicRoomId(request.ClinicRoomId);
            var patientId = new PatientId(request.PatientId);

            if (await _doctors.GetByIdAsync(
                    doctorId,
                    cancellationToken) is null)
            {
                return Result.Failure<Guid>(
                    Error.NotFound(
                        "Doctor.NotFound",
                        "Doctor was not found."));
            }

            if (await _clinicRooms.GetByIdAsync(
                    clinicRoomId,
                    cancellationToken) is null)
            {
                return Result.Failure<Guid>(
                    Error.NotFound(
                        "ClinicRoom.NotFound",
                        "Clinic room was not found."));
            }

            var period = DateRange.Create(
                request.StartUtc,
                request.EndUtc);

            if (await _appointments.HasDoctorConflictAsync(
                    doctorId,
                    period,
                    ct: cancellationToken))
            {
                return Result.Failure<Guid>(
                    Error.Conflict(
                        "Appointment.DoctorConflict",
                        "Doctor is already booked during this period."));
            }

            if (await _appointments.HasPatientConflictAsync(
                    patientId,
                    period,
                    ct: cancellationToken))
            {
                return Result.Failure<Guid>(
                    Error.Conflict(
                        "Appointment.PatientConflict",
                        "Patient already has an appointment during this period."));
            }

            if (await _appointments.HasClinicRoomConflictAsync(
                    clinicRoomId,period,ct: cancellationToken))
            {
                return Result.Failure<Guid>(
                    Error.Conflict("Appointment.ClinicRoomConflict",
                        "Clinic room is already booked during this period."));
            }

            var appointment = Appointment.Schedule(
                patientId,
                doctorId,
                clinicRoomId,
                period,
                request.Type,
                _dateTimeProvider.UtcNow,
                request.Reason);

            await _appointments.AddAsync(appointment,cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(appointment.Id.Value);
        }
    }

}
