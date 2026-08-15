using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.AdministrationHrPayroll.Ward
{
    public sealed class Ward : AggregateRoot<WardId>
    {
        public string Name { get; private set; } = null!;
        public DepartmentId DepartmentId { get; private set; } = null!;
        public int Capacity { get; private set; }

        private readonly List<RoomBedId> _bedIds = new();
        public IReadOnlyCollection<RoomBedId> BedIds => _bedIds.AsReadOnly();

        private Ward() { }

        private Ward(WardId id, string name, DepartmentId departmentId, int capacity) : base(id)
        {
            Name = name;
            DepartmentId = departmentId;
            Capacity = capacity;
        }

        public static Ward Create(string name, DepartmentId departmentId, int capacity)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Ward name is required.");
            if (capacity <= 0) throw new DomainException("Capacity must be greater than zero.");
            return new Ward(WardId.New(), name.Trim(), departmentId, capacity);
        }

        public void AssignBed(RoomBedId bedId)
        {
            if (_bedIds.Count >= Capacity) throw new DomainException($"Ward '{Name}' is at full bed capacity.");
            if (_bedIds.Contains(bedId)) throw new DomainException("This bed is already assigned to the ward.");
            _bedIds.Add(bedId);
        }

        public void RemoveBed(RoomBedId bedId) => _bedIds.Remove(bedId);
    }
}
