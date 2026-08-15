using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Appointments.GetDoctorSchedule
{
    public sealed record GetDoctorScheduleQuery(Guid DoctorId, DateTime Date) : IQuery<IReadOnlyList<AppointmentDto>>;
}
