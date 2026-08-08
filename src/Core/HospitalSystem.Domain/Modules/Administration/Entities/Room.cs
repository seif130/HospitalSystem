using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.Modules.Administration.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Administration.Entities
{
    public class Room : BaseEntity, IAggregateRoot
    {
        public Guid DepartmentId { get; private set; }
        public string RoomNumber { get; private set; } = default!;
        public RoomType Type { get; private set; }

        public Department Department { get; private set; } = default!;

        private readonly List<Bed> _beds = new();
        public IReadOnlyCollection<Bed> Beds => _beds.AsReadOnly();

        private Room() { }

        private Room(Guid departmentId, string roomNumber, RoomType type)
        {
            DepartmentId = departmentId;
            RoomNumber = roomNumber;
            Type = type;
        }

        internal static Result<Room> Create(Guid departmentId, string roomNumber, RoomType type)
        {
            var errors = new List<Error>();

            if (departmentId == Guid.Empty)
                errors.Add(Error.Validation("Room.EmptyDepartmentId", "Department ID is required."));
            if (string.IsNullOrWhiteSpace(roomNumber))
                errors.Add(Error.Validation("Room.EmptyRoomNumber", "Room number is required."));

            if (errors.Any())
                return Result<Room>.Fail(errors);

            return Result<Room>.Ok(new Room(departmentId, roomNumber, type));
        }

        public Result<Bed> AddBed(string bedNumber)
        {
            if (string.IsNullOrWhiteSpace(bedNumber))
                return Result<Bed>.Fail(Error.Validation("Bed.EmptyNumber", "Bed number is required."));

            if (_beds.Any(b => b.BedNumber == bedNumber))
                return Result<Bed>.Fail(Error.Validation("Bed.AlreadyExists", "Bed with this number already exists in this room."));

            var bedResult = Bed.Create(Id, bedNumber);
            if (!bedResult.IsSuccess)
                return Result<Bed>.Fail(bedResult.Errors);

            _beds.Add(bedResult.Data);
            LastModifiedAt = DateTime.UtcNow;

            return Result<Bed>.Ok(bedResult.Data);
        }
    }
}
