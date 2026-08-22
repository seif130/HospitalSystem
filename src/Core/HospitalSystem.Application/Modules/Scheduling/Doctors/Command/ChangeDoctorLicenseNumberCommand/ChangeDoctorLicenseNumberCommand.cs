using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Command.ChangeDoctorLicenseNumberCommand
{
    public sealed record ChangeDoctorLicenseNumberCommand(
        Guid DoctorId,
        string LicenseNumber)
        : ICommand;

}
