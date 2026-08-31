using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Policy;
using HospitalSystem.Domain.Reprository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Command.OfferWaitlistSlotCommand
{
    public sealed class OfferWaitlistSlotCommandHandler: ICommandHandler<OfferWaitlistSlotCommand>
    {
        private readonly IWaitlistRepository _waitlists;
        private readonly IAppointmentRepository _appointments;
        private readonly IUnitOfWork _unitOfWork;
        private readonly WaitlistPolicy _waitlistPolicy;

        public OfferWaitlistSlotCommandHandler(
            IWaitlistRepository waitlists,
            IAppointmentRepository appointments,
            IUnitOfWork unitOfWork,
            WaitlistPolicy waitlistPolicy)
        {
            _waitlists = waitlists;
            _appointments = appointments;
            _unitOfWork = unitOfWork;
            _waitlistPolicy = waitlistPolicy;
        }

        public async Task<Result> Handle(
            OfferWaitlistSlotCommand request,CancellationToken cancellationToken = default)
        {
            var waitlist = await _waitlists.GetByIdAsync(
                new WaitlistId(request.WaitlistId),cancellationToken);

            if (waitlist is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "Waitlist.NotFound",
                        "Waitlist entry was not found."));
            }

            var appointment = await _appointments.GetByIdAsync(
                new AppointmentId(request.AppointmentId),cancellationToken);

            if (appointment is null)
            {
                return Result.Failure(
                    Error.NotFound("Appointment.NotFound",
                        "Appointment was not found."));
            }

            if (!_waitlistPolicy.IsEligibleForAppointment(
                    waitlist,
                    appointment))
            {
                return Result.Failure(
                    Error.Validation(
                        "Waitlist.InvalidAppointment",
                        "The appointment is not eligible for this waitlist entry."));
            }

            waitlist.OfferSlot(appointment.Id);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
