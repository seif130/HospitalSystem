using HospitalSystem.Application.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.Command.RenameDepartment
{
    public sealed record RenameDepartmentCommand(
        Guid DepartmentId,
        string Name) : ICommand;

}
