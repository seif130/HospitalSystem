using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Command.ChangeDoctorDepartmentCommand
{
    public sealed record ChangeDoctorDepartmentCommand(
        Guid DoctorId,
        Guid DepartmentId)
        : ICommand;

}
