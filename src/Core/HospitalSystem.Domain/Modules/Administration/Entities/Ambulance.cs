using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.Modules.Administration.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Administration.Entities
{
    public class Ambulance : BaseEntity, IAggregateRoot
    {
        public string VehicleNumber { get; private set; } = default!;
        public string DriverName { get; private set; } = default!;
        public string PhoneNumber { get; private set; } = default!;
        public AmbulanceStatus Status { get; private set; }

        private Ambulance() { }

        private Ambulance(string vehicleNumber, string driverName, string phoneNumber)
        {
            VehicleNumber = vehicleNumber;
            DriverName = driverName;
            PhoneNumber = phoneNumber;
            Status = AmbulanceStatus.Available;
        }

        public static Result<Ambulance> Create(string vehicleNumber, string driverName, string phoneNumber)
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(vehicleNumber))
                errors.Add(Error.Validation("Ambulance.EmptyVehicleNumber", "Vehicle number is required."));
            if (string.IsNullOrWhiteSpace(driverName))
                errors.Add(Error.Validation("Ambulance.EmptyDriverName", "Driver name is required."));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                errors.Add(Error.Validation("Ambulance.EmptyPhoneNumber", "Phone number is required."));

            if (errors.Any())
                return Result<Ambulance>.Fail(errors);

            return Result<Ambulance>.Ok(new Ambulance(vehicleNumber, driverName, phoneNumber));
        }

        public Result UpdateStatus(AmbulanceStatus newStatus)
        {
            Status = newStatus;
            LastModifiedAt = DateTime.UtcNow;
            return Result.ok();
        }
    }
}
