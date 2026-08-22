using HospitalSystem.Domain.Modules.Scheduling.Appointments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.Dto
{
    public static class AppointmentMappings
    {
        public static AppointmentDto ToDto(this Appointment appointment)
        {
            return new AppointmentDto(
                appointment.Id.Value,
                appointment.PatientId.Value,
                appointment.DoctorId.Value,
                appointment.ClinicRoomId.Value,
                appointment.ScheduledPeriod.Start,
                appointment.ScheduledPeriod.End!.Value,
                appointment.Type,
                appointment.Status,
                appointment.Reason,
                appointment.CancellationReason);
        }
    }

}
