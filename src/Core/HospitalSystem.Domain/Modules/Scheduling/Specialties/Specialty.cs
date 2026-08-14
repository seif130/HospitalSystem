using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Specialties
{
    public sealed class Specialty : AggregateRoot<Guid>
    {
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public bool IsActive { get; private set; } = true;

        private Specialty() { }

        private Specialty(Guid id, string name, string? description) : base(id)
        {
            Name = name;
            Description = description;
        }

        public static Specialty Create(string name, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Specialty name is required.");
            return new Specialty(Guid.NewGuid(), name.Trim(), description?.Trim());
        }

        public void Deactivate() => IsActive = false;
        public void Reactivate() => IsActive = true;
    }
}
