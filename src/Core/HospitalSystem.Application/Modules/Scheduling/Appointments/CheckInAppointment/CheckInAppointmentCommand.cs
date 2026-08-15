using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.CheckInAppointment
{
    public sealed record CheckInAppointmentCommand(Guid AppointmentId) : ICommand;
}
