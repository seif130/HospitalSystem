using HospitalSystem.Application.Modules.Scheduling.EventHandlers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.CancelAppointment.Event
{
    public sealed class AppointmentCancelledDomainEventHandler : INotificationHandler<AppointmentCancelledDomainEvent>
    {
        private readonly ILogger<AppointmentCancelledDomainEventHandler> _logger;

        public AppointmentCancelledDomainEventHandler(ILogger<AppointmentCancelledDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(AppointmentCancelledDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Appointment {AppointmentId} cancelled. Reason: {Reason}. Checking waitlist...",
                notification.AppointmentId,
                notification.Reason);

            return Task.CompletedTask;
        }
    }
}
