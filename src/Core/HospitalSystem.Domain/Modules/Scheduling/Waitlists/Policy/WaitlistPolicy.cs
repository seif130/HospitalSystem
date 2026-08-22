using HospitalSystem.Domain.Modules.Scheduling.Appointments;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Enums;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Waitlists.Policy
{
    public sealed class WaitlistPolicy
    {
        public bool IsEligibleForAppointment( Waitlist waitlist, Appointment appointment)
        {
            if (waitlist.Status != WaitlistEntryStatus.Waiting)
                return false;

            if (waitlist.DoctorId != appointment.DoctorId)
                return false;

            if (appointment.Status != AppointmentStatus.Scheduled)
                return false;

            return waitlist.PreferredFromUtc <= appointment.ScheduledPeriod.Start
                   && waitlist.PreferredToUtc >= appointment.ScheduledPeriod.Start;
        }
    }

}
