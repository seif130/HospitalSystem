using HospitalSystem.Application.Modules.Scheduling.Doctors.Dto;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Quries.GetDoctorsBySpecialtyQuery
{
    public sealed class GetDoctorsBySpecialtyQueryHandler
     : IQueryHandler<
         GetDoctorsBySpecialtyQuery,
         IReadOnlyList<DoctorDto>>
    {
        private readonly IDoctorRepository _doctors;

        public GetDoctorsBySpecialtyQueryHandler(
            IDoctorRepository doctors)
        {
            _doctors = doctors;
        }

        public async Task<Result<IReadOnlyList<DoctorDto>>> Handle(
            GetDoctorsBySpecialtyQuery request,
            CancellationToken cancellationToken)
        {
            var doctors = await _doctors.GetBySpecialtyAsync(
                new SpecialtyId(request.SpecialtyId),
                cancellationToken);

            return doctors
                .Select(x => x.ToDto())
                .ToList();
        }
    }
}
