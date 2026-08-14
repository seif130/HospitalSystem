using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.ClinicRoom
{
    public sealed class ClinicRoom : AggregateRoot<ClinicRoomId>
    {
        public string RoomNumber { get; private set; } = null!;
        public DepartmentId DepartmentId { get; private set; } = null!;
        public int Capacity { get; private set; }

        private readonly List<DateRange> _bookings = new();
        public IReadOnlyCollection<DateRange> Bookings => _bookings.AsReadOnly();

        private ClinicRoom() { }

        private ClinicRoom(ClinicRoomId id, string roomNumber, DepartmentId departmentId, int capacity) : base(id)
        {
            RoomNumber = roomNumber;
            DepartmentId = departmentId;
            Capacity = capacity;
        }

        public static ClinicRoom Create(string roomNumber, DepartmentId departmentId, int capacity)
        {
            if (string.IsNullOrWhiteSpace(roomNumber)) throw new DomainException("Room number is required.");
            if (capacity <= 0) throw new DomainException("Capacity must be greater than zero.");
            return new ClinicRoom(ClinicRoomId.New(), roomNumber.Trim(), departmentId, capacity);
        }

        public void Book(DateRange slot)
        {
            if (_bookings.Any(b => b.Overlaps(slot)))
                throw new DomainException($"Room {RoomNumber} is already booked for the requested time.");
            _bookings.Add(slot);
        }

        public void ReleaseBooking(DateRange slot) => _bookings.Remove(slot);
    }
}
