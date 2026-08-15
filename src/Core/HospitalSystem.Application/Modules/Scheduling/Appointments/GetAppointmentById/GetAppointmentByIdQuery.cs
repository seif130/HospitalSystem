using HospitalSystem.Application.Modules.Scheduling.Appointments.GetDoctorSchedule;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.GetAppointmentById
{
    public sealed record GetAppointmentByIdQuery(Guid AppointmentId) : IQuery<AppointmentDto>;
}
