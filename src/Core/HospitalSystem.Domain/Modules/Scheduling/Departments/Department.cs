using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Departments
{
    public sealed class Department : AggregateRoot<DepartmentId>
    {
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }

        private Department()
        {
        }

        private Department( DepartmentId id, string name, string? description): base(id)
        {
            Name = name;
            Description = NormalizeOptional(description);
        }

        public static Department Create( string name,string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Department name is required.");

            return new Department(DepartmentId.New(), name.Trim(), NormalizeOptional(description));
        }

        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Department name is required.");

            Name = name.Trim();
        }

        public void UpdateDescription(string? description)
        {
            Description = NormalizeOptional(description);
        }

        private static string? NormalizeOptional(string? value)
        {
            return string.IsNullOrWhiteSpace(value)? null: value.Trim();
        }
    }

}
