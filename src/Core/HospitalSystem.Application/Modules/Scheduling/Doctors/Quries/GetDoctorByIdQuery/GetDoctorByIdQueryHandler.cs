using HospitalSystem.Application.Modules.Scheduling.Doctors.Dto;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Quries.GetDoctorByIdQuery
{
    public sealed class GetDoctorByIdQueryHandler
        : IQueryHandler<GetDoctorByIdQuery, DoctorDto>
    {
        private readonly IDoctorRepository _doctors;

        public GetDoctorByIdQueryHandler(
            IDoctorRepository doctors)
        {
            _doctors = doctors;
        }

        public async Task<Result<DoctorDto>> Handle(
            GetDoctorByIdQuery request,
            CancellationToken cancellationToken)
        {
            var doctor = await _doctors.GetByIdAsync(
                new DoctorId(request.DoctorId),
                cancellationToken);

            if (doctor is null)
            {
                return Result.Failure<DoctorDto>(
                    Error.NotFound(
                        "Doctor.NotFound",
                        "Doctor was not found."));
            }

            return doctor.ToDto();
        }
    }

}
