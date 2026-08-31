using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Enums;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Appointments.Policy
{
    public sealed class AppointmentConflictPolicy
    {
        public bool HasConflict(DoctorId doctorId,
            ClinicRoomId clinicRoomId,DateRange requestedPeriod,IEnumerable<Appointment> existingAppointments)
        {
            ArgumentNullException.ThrowIfNull(requestedPeriod);
            ArgumentNullException.ThrowIfNull(existingAppointments);

            return existingAppointments.Any(appointment =>
            {
                if (appointment.Status is AppointmentStatus.Cancelled or AppointmentStatus.NoShow)
                {
                    return false;
                }

                if (!appointment.ScheduledPeriod.Overlaps(requestedPeriod))
                {
                    return false;
                }

                return appointment.DoctorId == doctorId || appointment.ClinicRoomId == clinicRoomId;
            });
        }
    }

}
