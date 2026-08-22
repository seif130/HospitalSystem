using HospitalSystem.Domain.Modules.Scheduling.Appointments.Enums;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Appointments.Policy
{
    public sealed class AppointmentConflictPolicy
    {
        public bool HasConflict( DateRange requestedPeriod,IEnumerable<Appointment> existingAppointments)
        {
            return existingAppointments.Any(appointment =>
            {
                if (appointment.Status is AppointmentStatus.Cancelled or AppointmentStatus.NoShow)
                {
                    return false;
                }

                return appointment.ScheduledPeriod.Overlaps(requestedPeriod);
            });
        }
    }

}
