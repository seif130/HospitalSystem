using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Command.CreateDoctorCommand
{
    public sealed record CreateDoctorCommand(
        string FirstName,
        string LastName,
        MedicalSpecialty Specialty,
        Guid DepartmentId,
        string LicenseNumber)
        : ICommand<Guid>;

}
