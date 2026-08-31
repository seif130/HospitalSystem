using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Command.CreateDoctorCommand
{
    public sealed record CreateDoctorCommand(
        string FirstName,
        string LastName,
        Guid SpecialtyId,
        Guid DepartmentId,
        string LicenseNumber)
        : ICommand<Guid>;
}
