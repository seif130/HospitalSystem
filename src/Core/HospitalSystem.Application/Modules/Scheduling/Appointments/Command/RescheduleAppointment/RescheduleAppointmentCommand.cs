using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ICommand = HospitalSystem.Application.Shared.Messaging.ICommand;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.RescheduleAppointment
{
    public sealed record RescheduleAppointmentCommand(
    Guid AppointmentId,
    DateTime StartUtc,
    DateTime EndUtc) : ICommand;
   
}
