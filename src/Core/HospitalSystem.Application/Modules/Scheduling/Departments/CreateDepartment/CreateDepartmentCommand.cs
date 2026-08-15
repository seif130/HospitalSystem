using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.CreateDepartment
{
    public sealed record CreateDepartmentCommand(string Name, string Description) : ICommand<Guid>;
}
