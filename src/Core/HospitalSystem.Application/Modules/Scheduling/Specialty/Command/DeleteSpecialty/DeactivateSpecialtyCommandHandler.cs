using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Specialties.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Command.DeleteSpecialty
{
    public sealed class DeactivateSpecialtyCommandHandler
           : ICommandHandler<DeactivateSpecialtyCommand>
    {
        private readonly ISpecialtyRepository _specialties;

        public DeactivateSpecialtyCommandHandler(
            ISpecialtyRepository specialties)
        {
            _specialties = specialties;
        }

        public async Task<Result> Handle(
            DeactivateSpecialtyCommand request,
            CancellationToken cancellationToken)
        {
            var specialty = await _specialties.GetByIdAsync(
                new SpecialtyId(request.SpecialtyId),
                cancellationToken);

            if (specialty is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "Specialty.NotFound",
                        "Specialty was not found."));
            }

            specialty.Deactivate();

            return Result.Success();
        }
    }
}
