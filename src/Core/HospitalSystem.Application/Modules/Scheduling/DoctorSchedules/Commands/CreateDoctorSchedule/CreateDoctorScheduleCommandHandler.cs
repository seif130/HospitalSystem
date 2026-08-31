using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.DoctorSchedules.Commands.CreateDoctorSchedule
{
    public sealed class CreateDoctorScheduleCommandHandler
        : ICommandHandler<CreateDoctorScheduleCommand, Guid>
    {
        private readonly IDoctorRepository _doctors;
        private readonly IDoctorScheduleRepository _schedules;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDoctorScheduleCommandHandler(
            IDoctorRepository doctors,
            IDoctorScheduleRepository schedules,
            IUnitOfWork unitOfWork)
        {
            _doctors = doctors;
            _schedules = schedules;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            CreateDoctorScheduleCommand request,
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

            var schedule = DoctorSchedule.Create(
                doctorId,
                request.DayOfWeek,
                request.StartTime,
                request.EndTime);

            await _schedules.AddAsync(
                schedule,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success(
                schedule.Id.Value);
        }
    }
}
