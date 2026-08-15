using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.RescheduleAppointment
{
    public sealed record RescheduleAppointmentCommand(Guid AppointmentId, DateTime NewTimeUtc) : ICommand;
}
