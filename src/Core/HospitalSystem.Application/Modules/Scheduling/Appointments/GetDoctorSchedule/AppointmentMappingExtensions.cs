using HospitalSystem.Domain.Modules.Scheduling.Appointments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.GetDoctorSchedule
{
    public static class AppointmentMappingExtensions
    {
        public static AppointmentDto ToDto(this Appointment a) => new(
            a.Id.Value,
            a.PatientId.Value,
            a.DoctorId.Value,
            a.ClinicRoomId.Value,
            a.ScheduledAtUtc,
            a.Status.ToString()
        );
    }
}
