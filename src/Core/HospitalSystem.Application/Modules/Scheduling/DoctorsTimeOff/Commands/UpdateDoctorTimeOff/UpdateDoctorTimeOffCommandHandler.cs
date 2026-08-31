using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorsTimeOff.Commands.UpdateDoctorTimeOff
{
    public sealed class UpdateDoctorTimeOffCommandHandler
        : ICommandHandler<UpdateDoctorTimeOffCommand>
    {
        private readonly IDoctorTimeOffRepository _timeOffs;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDoctorTimeOffCommandHandler(
            IDoctorTimeOffRepository timeOffs,
            IUnitOfWork unitOfWork)
        {
            _timeOffs = timeOffs;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UpdateDoctorTimeOffCommand request,
            CancellationToken cancellationToken)
        {
            var timeOff = await _timeOffs.GetByIdAsync(
                new DoctorTimeOffId(request.DoctorTimeOffId),
                cancellationToken);

            if (timeOff is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "DoctorTimeOff.NotFound",
                        "Doctor time off was not found."));
            }

            var period = DateRange.Create(
                request.FromUtc,
                request.ToUtc);

            var hasConflict = await _timeOffs.HasConflictAsync(
                timeOff.DoctorId,
                period,
                timeOff.Id,
                cancellationToken);

            if (hasConflict)
            {
                return Result.Failure(
                    Error.Conflict(
                        "DoctorTimeOff.Conflict",
                        "Doctor already has time off during this period."));
            }

            timeOff.UpdatePeriod(period);
            timeOff.UpdateReason(request.Reason);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }
    }
}
