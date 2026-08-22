using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.Command.CreateDepartment
{
    public sealed record CreateDepartmentCommand(
           string Name,
           string? Description = null) : ICommand<Guid>;

}
