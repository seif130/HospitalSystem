using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Appointments.Enums
{
    public enum AppointmentType
    {
        Consultation = 1,
        FollowUp = 2,
        CheckUp = 3,
        Procedure = 4,
        Emergency = 5,
        Telemedicine = 6
    }

}
