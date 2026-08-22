using HospitalSystem.Application.Modules.Scheduling.Specialties;
using HospitalSystem.Application.Modules.Scheduling.Specialty.Dto;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Specialties.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Quires.GetSpecialtyById
{
    public sealed class GetSpecialtyByIdQueryHandler
           : IQueryHandler<
               GetSpecialtyByIdQuery,
               SpecialtyDto>
    {
        private readonly ISpecialtyRepository _specialties;

        public GetSpecialtyByIdQueryHandler(
            ISpecialtyRepository specialties)
        {
            _specialties = specialties;
        }

        public async Task<Result<SpecialtyDto>> Handle(
            GetSpecialtyByIdQuery request,
            CancellationToken cancellationToken)
        {
            var specialty = await _specialties.GetByIdAsync(
                new SpecialtyId(request.SpecialtyId),
                cancellationToken);

            if (specialty is null)
            {
                return Result.Failure<SpecialtyDto>(
                    Error.NotFound(
                        "Specialty.NotFound",
                        "Specialty was not found."));
            }

            return specialty.ToDto();
        }
    }
}
