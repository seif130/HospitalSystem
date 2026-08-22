using HospitalSystem.Domain.Modules.Scheduling.Departments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Departments.Dto
{
    public static class DepartmentMappings
    {
        public static DepartmentDto ToDto(this Department department)
        {
            return new DepartmentDto(
                department.Id.Value,
                department.Name);
        }
    }

}
