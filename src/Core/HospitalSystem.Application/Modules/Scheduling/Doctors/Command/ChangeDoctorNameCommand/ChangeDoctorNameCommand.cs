using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Command.ChangeDoctorNameCommand
{
    public sealed record ChangeDoctorNameCommand(
        Guid DoctorId,
        string FirstName,
        string LastName)
        : ICommand;

}
