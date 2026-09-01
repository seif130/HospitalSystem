using HospitalSystem.Domain.Enums;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Clinic.Patients.Enum;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.Patients
{
    public sealed class Patient : AggregateRoot<PatientId>
    {
        public PersonName Name { get; private set; } = null!;
        public DateTime DateOfBirth { get; private set; }
        public Gender Gender { get; private set; }
        public BloodType? BloodType { get; private set; }
        public PhoneNumber Phone { get; private set; } = null!;
        public EmailAddress? Email { get; private set; }
        public Address Address { get; private set; } = null!;
        public string? EmergencyContactName { get; private set; }
        public PhoneNumber? EmergencyContactPhone { get; private set; }
        public PatientStatus Status { get; private set; }

        private Patient() : base(PatientId.New()) { } // EF Core

        private Patient(PatientId id, PersonName name, DateTime dateOfBirth, Gender gender,
            PhoneNumber phone, Address address) : base(id)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Phone = phone;
            Address = address;
            Status = PatientStatus.Active;
        }

        public static Patient Register(PersonName name, DateTime dateOfBirth, Gender gender,
            PhoneNumber phone, Address address)
        {
            if (dateOfBirth > DateTime.UtcNow)
                throw new DomainException("Date of birth cannot be in the future.");

            var patient = new Patient(PatientId.New(), name, dateOfBirth, gender, phone, address);
            patient.AddDomainEvent(new PatientRegisteredDomainEvent(patient.Id));
            return patient;
        }

        public void SetEmergencyContact(string name, PhoneNumber phone)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Emergency contact name is required.");
            EmergencyContactName = name.Trim();
            EmergencyContactPhone = phone;
        }

        public void SetBloodType(BloodType bloodType) => BloodType = bloodType;

        public void MarkAsAdmitted()
        {
            if (Status == PatientStatus.Deceased) throw new DomainException("Cannot admit a deceased patient.");
            Status = PatientStatus.Admitted;
        }

        public void MarkAsDischarged()
        {
            if (Status != PatientStatus.Admitted) throw new DomainException("Only an admitted patient can be discharged.");
            Status = PatientStatus.Discharged;
            AddDomainEvent(new PatientDischargedDomainEvent(Id));
        }
    }
}
