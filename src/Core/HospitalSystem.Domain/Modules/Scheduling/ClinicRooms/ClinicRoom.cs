using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.ClinicRooms
{
    public sealed class ClinicRoom : AggregateRoot<ClinicRoomId>
    {
        public string RoomNumber { get; private set; } = null!;
        public DepartmentId DepartmentId { get; private set; } = null!;
        public int Capacity { get; private set; }

        private ClinicRoom()
        {
        }

        private ClinicRoom(
            ClinicRoomId id,
            string roomNumber,
            DepartmentId departmentId,
            int capacity)
            : base(id)
        {
            RoomNumber = roomNumber;
            DepartmentId = departmentId;
            Capacity = capacity;
        }

        public static ClinicRoom Create(
            string roomNumber,
            DepartmentId departmentId,
            int capacity)
        {
            if (string.IsNullOrWhiteSpace(roomNumber))
                throw new DomainException(
                    "Room number is required.");

            if (capacity <= 0)
                throw new DomainException(
                    "Capacity must be greater than zero.");

            return new ClinicRoom(
                ClinicRoomId.New(),
                roomNumber.Trim(),
                departmentId,
                capacity);
        }

        public void Rename(string roomNumber)
        {
            if (string.IsNullOrWhiteSpace(roomNumber))
                throw new DomainException("Room number is required.");

            RoomNumber = roomNumber.Trim();
        }

        public void ChangeCapacity(int capacity)
        {
            if (capacity <= 0)
                throw new DomainException("Capacity must be greater than zero.");

            Capacity = capacity;
        }

        public void ChangeDepartment(DepartmentId departmentId)
        {
            DepartmentId = departmentId;
        }
    }

}
