using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorSchedules.Commands.UpdateDoctorSchedule
{
    public sealed class UpdateDoctorScheduleCommandHandler
        : ICommandHandler<UpdateDoctorScheduleCommand>
    {
        private readonly IDoctorScheduleRepository _schedules;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDoctorScheduleCommandHandler(
            IDoctorScheduleRepository schedules,
            IUnitOfWork unitOfWork)
        {
            _schedules = schedules;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UpdateDoctorScheduleCommand request,
            CancellationToken cancellationToken)
        {
            var schedule = await _schedules.GetByIdAsync(
                new DoctorScheduleId(request.DoctorScheduleId),
                cancellationToken);

            if (schedule is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "DoctorSchedule.NotFound",
                        "Doctor schedule was not found."));
            }

            schedule.Update(
                request.DayOfWeek,
                request.StartTime,
                request.EndTime);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }
    }
}
