using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorsTimeOff.Commands.DeleteDoctorTimeOff
{
    public sealed class DeleteDoctorTimeOffCommandHandler
        : ICommandHandler<DeleteDoctorTimeOffCommand>
    {
        private readonly IDoctorTimeOffRepository _timeOffs;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDoctorTimeOffCommandHandler(
            IDoctorTimeOffRepository timeOffs,
            IUnitOfWork unitOfWork)
        {
            _timeOffs = timeOffs;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            DeleteDoctorTimeOffCommand request,
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

            _timeOffs.Remove(timeOff);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }
    }
}
