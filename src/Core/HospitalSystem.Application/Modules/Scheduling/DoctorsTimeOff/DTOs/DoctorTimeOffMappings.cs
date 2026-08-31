using HospitalSystem.Domain.Modules.Scheduling.Doctors;
using System;
using System.Collections.Generic;
using System.Text;


namespace HospitalSystem.Application.Modules.Scheduling.DoctorsTimeOff.DTOs
{
    public static class DoctorTimeOffMappings
    {
        public static DoctorTimeOffDto ToDto(
            this DoctorTimeOff timeOff)
        {
            return new DoctorTimeOffDto(
                timeOff.Id.Value,
                timeOff.DoctorId.Value,
                timeOff.Period.Start,
                timeOff.Period.End,
                timeOff.Reason);
        }
    }
}
