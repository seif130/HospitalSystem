using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.CancelAppointment
{
    public sealed record CancelAppointmentCommand(Guid AppointmentId, string Reason) : ICommand;
}
