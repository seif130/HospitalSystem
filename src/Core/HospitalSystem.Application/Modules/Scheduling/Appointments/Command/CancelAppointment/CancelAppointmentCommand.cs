using System;
using System.Collections.Generic;
using System.Text;
using ICommand = HospitalSystem.Application.Shared.Messaging.ICommand;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.CancelAppointment
{
    public sealed record CancelAppointmentCommand(
     Guid AppointmentId,
     string Reason) : ICommand;

}
