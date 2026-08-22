using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ICommand = HospitalSystem.Application.Shared.Messaging.ICommand;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Command.CompleteAppointment
{
    public sealed record CompleteAppointmentCommand(
        Guid AppointmentId) : ICommand;
}
