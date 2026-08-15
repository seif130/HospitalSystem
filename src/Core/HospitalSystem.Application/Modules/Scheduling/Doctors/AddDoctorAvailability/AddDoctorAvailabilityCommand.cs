using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.AddDoctorAvailability
{
    public sealed record AddDoctorAvailabilityCommand(Guid DoctorId, DateTime StartUtc, DateTime EndUtc) : ICommand;
}
