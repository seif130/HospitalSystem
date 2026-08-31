using HospitalSystem.Application.Modules.Scheduling.Specialties;
using HospitalSystem.Application.Modules.Scheduling.Specialty.Dto;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Scheduling.Specialties.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Quires.GetSpecialties
{
    public sealed class GetSpecialtiesQueryHandler: IQueryHandler<GetSpecialtiesQuery,IReadOnlyList<SpecialtyDto>>
    {
        private readonly ISpecialtyRepository _specialties;

        public GetSpecialtiesQueryHandler(
            ISpecialtyRepository specialties)
        {
            _specialties = specialties;
        }

        public async Task<Result<IReadOnlyList<SpecialtyDto>>> Handle(
            GetSpecialtiesQuery request,CancellationToken cancellationToken = default)
        {
            var specialties = await _specialties.GetAllAsync(
                cancellationToken);

            var result = specialties
                .Select(x => x.ToDto())
                .ToList();

            return result;
        }
    }
}
