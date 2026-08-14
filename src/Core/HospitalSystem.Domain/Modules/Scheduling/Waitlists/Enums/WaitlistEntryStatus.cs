using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Waitlists.Enums
{
    public enum WaitlistEntryStatus
    {
        Waiting,
        Offered,
        Booked,
        Expired,
        Cancelled
    }
}
