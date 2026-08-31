using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Specialties.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Command.RenameSpecialty
{
    public sealed class RenameSpecialtyCommandHandler
     : ICommandHandler<RenameSpecialtyCommand>
    {
        private readonly ISpecialtyRepository _specialties;
        private readonly IUnitOfWork _unitOfWork;

        public RenameSpecialtyCommandHandler(
            ISpecialtyRepository specialties,
            IUnitOfWork unitOfWork)
        {
            _specialties = specialties;
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }
    }
}
