using HospitalSystem.Domain.Modules.Scheduling.Department;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.GetDepartments
{
    public static class DepartmentMappingExtensions
    {
        public static DepartmentDto ToDto(this Department department) => new(
            department.Id.Value,
            department.Name,
            department.Description
        );
    }
}
