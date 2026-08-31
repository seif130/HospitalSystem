using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Specialties
{
    public sealed class Specialty : AggregateRoot<SpecialtyId>
    {
        public string Name { get; private set; } = null!;

        public string? Description { get; private set; }

        public bool IsActive { get; private set; }

        private Specialty()
        {
        }

        private Specialty(
            SpecialtyId id,
            string name,
            string? description)
            : base(id)
        {
            Name = name;
            Description = NormalizeOptional(description);
            IsActive = true;
        }

        public static Specialty Create(
            string name,
            string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException(
                    "Specialty name is required.");
            }

            return new Specialty(
                SpecialtyId.New(),
                name.Trim(),
                description);
        }

        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException(
                    "Specialty name is required.");
            }

            Name = name.Trim();
        }

        public void UpdateDescription(string? description)
        {
            Description = NormalizeOptional(description);
        }

        public void Deactivate()
        {
            if (!IsActive)
                return;

            IsActive = false;
        }

        public void Reactivate()
        {
            if (IsActive)
                return;

            IsActive = true;
        }

        private static string? NormalizeOptional(string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? null
                : value.Trim();
        }
    }

}
