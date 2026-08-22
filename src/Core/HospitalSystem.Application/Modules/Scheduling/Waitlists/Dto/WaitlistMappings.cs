using HospitalSystem.Domain.Modules.Scheduling.Waitlists;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Waitlists.Dto
{
    public static class WaitlistMappings
    {
        public static WaitlistDto ToDto(this Waitlist waitlist)
        {
            return new WaitlistDto(
                waitlist.Id.Value,
                waitlist.PatientId.Value,
                waitlist.DoctorId.Value,
                waitlist.PreferredFromUtc,
                waitlist.PreferredToUtc,
                waitlist.JoinedOnUtc,
                waitlist.Status);
        }
    }

}
