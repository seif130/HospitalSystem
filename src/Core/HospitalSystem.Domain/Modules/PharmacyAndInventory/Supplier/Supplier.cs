using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.PharmacyAndInventory.Supplier
{
    public sealed class Supplier : AggregateRoot<SupplierId>
    {
        public string Name { get; private set; } = null!;
        public PhoneNumber Phone { get; private set; } = null!;
        public EmailAddress Email { get; private set; } = null!;
        public Address Address { get; private set; } = null!;
        public bool IsActive { get; private set; } = true;

        private Supplier() { }

        private Supplier(SupplierId id, string name, PhoneNumber phone, EmailAddress email, Address address) : base(id)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
        }

        public static Supplier Register(string name, PhoneNumber phone, EmailAddress email, Address address)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Supplier name is required.");
            return new Supplier(SupplierId.New(), name.Trim(), phone, email, address);
        }

        public void Deactivate() => IsActive = false;
        public void Reactivate() => IsActive = true;
    }

}
