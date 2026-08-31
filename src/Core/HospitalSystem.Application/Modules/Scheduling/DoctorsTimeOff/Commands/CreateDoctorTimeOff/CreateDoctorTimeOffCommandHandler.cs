using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorsTimeOff.Commands.CreateDoctorTimeOff
{
    public sealed class CreateDoctorTimeOffCommandHandler
        : ICommandHandler<CreateDoctorTimeOffCommand, Guid>
    {
        private readonly IDoctorRepository _doctors;
        private readonly IDoctorTimeOffRepository _timeOffs;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDoctorTimeOffCommandHandler(
            IDoctorRepository doctors,
            IDoctorTimeOffRepository timeOffs,
            IUnitOfWork unitOfWork)
        {
            _doctors = doctors;
            _timeOffs = timeOffs;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            CreateDoctorTimeOffCommand request,
            CancellationToken cancellationToken)
        {
            var doctorId = new DoctorId(request.DoctorId);

            var doctor = await _doctors.GetByIdAsync(
                doctorId,
                cancellationToken);

            if (doctor is null)
            {
                return Result.Failure<Guid>(
                    Error.NotFound(
                        "Doctor.NotFound",
                        "Doctor was not found."));
            }

            var period = DateRange.Create(
                request.FromUtc,
                request.ToUtc);

            var hasConflict = await _timeOffs.HasConflictAsync(
                doctorId,
                period,
                null,
                cancellationToken);

            if (hasConflict)
            {
                return Result.Failure<Guid>(
                    Error.Conflict(
                        "DoctorTimeOff.Conflict",
                        "Doctor already has time off during this period."));
            }

            var timeOff = DoctorTimeOff.Create(
                doctorId,
                period,
                request.Reason);

            await _timeOffs.AddAsync(
                timeOff,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success(
                timeOff.Id.Value);
        }
    }
}
