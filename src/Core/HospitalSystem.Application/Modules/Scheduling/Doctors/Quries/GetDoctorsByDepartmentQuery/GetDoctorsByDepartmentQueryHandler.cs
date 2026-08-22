using HospitalSystem.Application.Modules.Scheduling.Doctors.Dto;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Quries.GetDoctorsByDepartmentQuery
{
    public sealed class GetDoctorsByDepartmentQueryHandler
        : IQueryHandler<GetDoctorsByDepartmentQuery,IReadOnlyList<DoctorDto>>
    {
        private readonly IDoctorRepository _doctors;

        public GetDoctorsByDepartmentQueryHandler(
            IDoctorRepository doctors)
        {
            _doctors = doctors;
        }

        public async Task<Result<IReadOnlyList<DoctorDto>>> Handle(
            GetDoctorsByDepartmentQuery request,CancellationToken cancellationToken)
        {
            var doctors = await _doctors.GetByDepartmentAsync(
                new DepartmentId(request.DepartmentId),cancellationToken);

            return doctors.Select(x => x.ToDto()).ToList();
        }
    }

}
