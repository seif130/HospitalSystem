using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.Modules.Administration.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Administration.Entities
{
    public class Bed : BaseEntity
    {
        public Guid RoomId { get; private set; }
        public string BedNumber { get; private set; } = default!;
        public BedStatus Status { get; private set; }

        public Room Room { get; private set; } = default!;

        private Bed() { }

        private Bed(Guid roomId, string bedNumber)
        {
            RoomId = roomId;
            BedNumber = bedNumber;
            Status = BedStatus.Available;
        }

        internal static Result<Bed> Create(Guid roomId, string bedNumber)
        {
            if (roomId == Guid.Empty)
                return Result<Bed>.Fail(Error.Validation("Bed.EmptyRoomId", "Room ID is required."));
            if (string.IsNullOrWhiteSpace(bedNumber))
                return Result<Bed>.Fail(Error.Validation("Bed.EmptyNumber", "Bed number is required."));

            return Result<Bed>.Ok(new Bed(roomId, bedNumber));
        }

        public Result UpdateStatus(BedStatus newStatus)
        {
            Status = newStatus;
            LastModifiedAt = DateTime.UtcNow;
            return Result.ok();
        }
    }
}
