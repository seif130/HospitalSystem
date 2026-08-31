using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Scheduling.Specialties.Contract;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.Modules.Scheduling.Specialties;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Command.CreateSpecialty
{
    public sealed class CreateSpecialtyCommandHandler
      : ICommandHandler<CreateSpecialtyCommand, Guid>
    {
        private readonly ISpecialtyRepository _specialties;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSpecialtyCommandHandler(
            ISpecialtyRepository specialties,
            IUnitOfWork unitOfWork)
        {
            _specialties = specialties;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            CreateSpecialtyCommand request,
            CancellationToken cancellationToken)
        {
            var normalizedName = request.Name.Trim();

            var exists = await _specialties.ExistsByNameAsync(
                normalizedName,
                cancellationToken);

            if (exists)
            {
                return Result.Failure<Guid>(
                    Error.Conflict(
                        "Specialty.AlreadyExists",
                        "A specialty with this name already exists."));
            }

            var specialty = Domain.Modules.Scheduling.Specialties.Specialty.Create(
         normalizedName,
         request.Description);

            await _specialties.AddAsync(
                specialty,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success(
                specialty.Id.Value);
        }
    }
}
