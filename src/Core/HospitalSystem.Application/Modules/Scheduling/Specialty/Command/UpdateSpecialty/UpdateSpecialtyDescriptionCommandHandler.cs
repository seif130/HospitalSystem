using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Specialties.Contract;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Specialty.Command.UpdateSpecialty
{
    public sealed class UpdateSpecialtyDescriptionCommandHandler
     : ICommandHandler<UpdateSpecialtyDescriptionCommand>
    {
        private readonly ISpecialtyRepository _specialties;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSpecialtyDescriptionCommandHandler(
            ISpecialtyRepository specialties,
            IUnitOfWork unitOfWork)
        {
            _specialties = specialties;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UpdateSpecialtyDescriptionCommand request,
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

            specialty.UpdateDescription(
                request.Description);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
