using System;
using System.Collections.Generic;
using System.Text;
using ICommand = HospitalSystem.Application.Shared.Messaging.ICommand;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.CheckInAppointment
{
    public sealed record CheckInAppointmentCommand(
    Guid AppointmentId) : ICommand;

}
