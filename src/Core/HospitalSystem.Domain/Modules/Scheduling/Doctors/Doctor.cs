using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Enums;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Doctors
{
    public sealed class Doctor : AggregateRoot<DoctorId>
    {
        public PersonName Name { get; private set; } = null!;
        public MedicalSpecialty Specialty { get; private set; }
        public DepartmentId DepartmentId { get; private set; } = null!;
        public string LicenseNumber { get; private set; } = null!;

        private readonly List<DateRange> _availabilitySlots = new();
        public IReadOnlyCollection<DateRange> AvailabilitySlots => _availabilitySlots.AsReadOnly();

        private Doctor() { }

        private Doctor(DoctorId id, PersonName name, MedicalSpecialty specialty, DepartmentId departmentId, string licenseNumber) : base(id)
        {
            Name = name;
            Specialty = specialty;
            DepartmentId = departmentId;
            LicenseNumber = licenseNumber;
        }

        public static Doctor Register(PersonName name, MedicalSpecialty specialty, DepartmentId departmentId, string licenseNumber)
        {
            if (string.IsNullOrWhiteSpace(licenseNumber)) throw new DomainException("License number is required.");
            return new Doctor(DoctorId.New(), name, specialty, departmentId, licenseNumber.Trim());
        }

        public void AddAvailability(DateRange slot)
        {
            if (_availabilitySlots.Any(s => s.Overlaps(slot)))
                throw new DomainException("This availability slot overlaps with an existing one.");
            _availabilitySlots.Add(slot);
        }

        public bool IsAvailable(DateTime moment) =>
            _availabilitySlots.Any(s => s.Start <= moment && (s.End is null || s.End >= moment));

        public void RemoveAvailability(DateRange slot) => _availabilitySlots.Remove(slot);
    }
}
