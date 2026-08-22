using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Specialties.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Command.RenameSpecialty
{
    public sealed class RenameSpecialtyCommandHandler: ICommandHandler<RenameSpecialtyCommand>
    {
        private readonly ISpecialtyRepository _specialties;

        public RenameSpecialtyCommandHandler(
            ISpecialtyRepository specialties)
        {
            _specialties = specialties;
        }

        public async Task<Result> Handle(
            RenameSpecialtyCommand request,
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

            var normalizedName = request.Name.Trim();

            if (string.Equals(
                specialty.Name,
                normalizedName,
                StringComparison.OrdinalIgnoreCase))
            {
                return Result.Success();
            }

            var exists = await _specialties.ExistsByNameAsync(
                normalizedName,
                cancellationToken);

            if (exists)
            {
                return Result.Failure(
                    Error.Conflict(
                        "Specialty.AlreadyExists",
                        "A specialty with this name already exists."));
            }

            specialty.Rename(normalizedName);

            return Result.Success();
        }
    }
}
