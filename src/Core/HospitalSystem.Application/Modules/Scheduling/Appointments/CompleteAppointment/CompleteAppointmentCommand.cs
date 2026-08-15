using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.CompleteAppointment
{
    public sealed record CompleteAppointmentCommand(Guid AppointmentId) : ICommand;
}
