using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Clinic.Nurses.Enum;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.Nurses
{
    public sealed class Nurse : AggregateRoot<NurseId>
    {
        public PersonName Name { get; private set; } = null!;
        public NurseSpecialization Specialization { get; private set; }
        public DepartmentId DepartmentId { get; private set; } = null!;

        private readonly List<DateRange> _shifts = new();
        public IReadOnlyCollection<DateRange> Shifts => _shifts.AsReadOnly();

        private Nurse() : base(NurseId.New()) { }

        private Nurse(NurseId id, PersonName name, NurseSpecialization specialization, DepartmentId departmentId) : base(id)
        {
            Name = name;
            Specialization = specialization;
            DepartmentId = departmentId;
        }

        public static Nurse Hire(PersonName name, NurseSpecialization specialization, DepartmentId departmentId) =>
            new(NurseId.New(), name, specialization, departmentId);

        public void AssignShift(DateRange shift)
        {
            if (_shifts.Any(s => s.Overlaps(shift)))
                throw new DomainException("Shift overlaps with an existing assignment for this nurse.");
            _shifts.Add(shift);
        }

        public void Reassign(DepartmentId departmentId) => DepartmentId = departmentId;
    }
}
