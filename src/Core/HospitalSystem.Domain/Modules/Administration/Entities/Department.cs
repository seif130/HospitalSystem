using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalSystem.Domain.Modules.Administration.Entities
{
        public class Department : BaseEntity, IAggregateRoot
        {
            public string Name { get; private set; } = default!;
            public string Description { get; private set; } = default!;
            public Guid? HeadDoctorId { get; private set; }

            private readonly List<Doctor> _doctors = new();
            public IReadOnlyCollection<Doctor> Doctors => _doctors.AsReadOnly();

            private readonly List<Nurse> _nurses = new();
            public IReadOnlyCollection<Nurse> Nurses => _nurses.AsReadOnly();

            private readonly List<Room> _rooms = new();
            public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();

            private readonly List<DepartmentEquipment> _equipments = new();
            public IReadOnlyCollection<DepartmentEquipment> Equipments => _equipments.AsReadOnly();

            private readonly List<DepartmentService> _services = new();
            public IReadOnlyCollection<DepartmentService> Services => _services.AsReadOnly();

            private readonly List<OnCallSchedule> _schedules = new();
            public IReadOnlyCollection<OnCallSchedule> Schedules => _schedules.AsReadOnly();

            private Department() { }

            private Department(string name, string description, Guid? headDoctorId)
            {
                Name = name;
                Description = description;
                HeadDoctorId = headDoctorId;
            }

            public static Result<Department> Create(string name, string description, Guid? headDoctorId = null)
            {
                if (string.IsNullOrWhiteSpace(name))
                    return Result<Department>.Fail(Error.Validation("Department.EmptyName", "Department name is required."));

                return Result<Department>.Ok(new Department(name, description ?? string.Empty, headDoctorId));
            }

            public Result UpdateDetails(string name, string description, Guid? headDoctorId)
            {
                if (string.IsNullOrWhiteSpace(name))
                    return Result.Fail(Error.Validation("Department.EmptyName", "Department name is required."));

                Name = name;
                Description = description ?? string.Empty;
                HeadDoctorId = headDoctorId;
                LastModifiedAt = DateTime.UtcNow;

                return Result.ok();
            }

            // ==================== Rooms Management ====================
            public Result<Room> AddRoom(string roomNumber, Enums.RoomType type)
            {
                var roomResult = Room.Create(Id, roomNumber, type);
                if (!roomResult.IsSuccess)
                    return Result<Room>.Fail(roomResult.Errors);

                _rooms.Add(roomResult.Data);
                LastModifiedAt = DateTime.UtcNow;

                return Result<Room>.Ok(roomResult.Data);
            }

        public Result RemoveRoom(Guid roomId)
        {
            var roomToRemove = _rooms.FirstOrDefault(r => r.Id == roomId);
            if (roomToRemove == null)
                return Result.Fail(Error.NotFound("Room.NotFound", "The room was not found in this department."));

            _rooms.Remove(roomToRemove);
            LastModifiedAt = DateTime.UtcNow;

            return Result.ok(); 
        }

        // ==================== Equipment Management ====================
        public Result<DepartmentEquipment> AddEquipment(string equipmentName, string serialNumber, DateTime purchaseDate)
            {
                var equipmentResult = DepartmentEquipment.Create(Id, equipmentName, serialNumber, purchaseDate);
                if (!equipmentResult.IsSuccess)
                    return Result<DepartmentEquipment>.Fail(equipmentResult.Errors);

                _equipments.Add(equipmentResult.Data);
                LastModifiedAt = DateTime.UtcNow;

                return Result<DepartmentEquipment>.Ok(equipmentResult.Data);
            }

            public void RemoveEquipment(Guid equipmentId)
            {
                var equipmentToRemove = _equipments.FirstOrDefault(de => de.Id == equipmentId);
                if (equipmentToRemove != null)
                {
                    _equipments.Remove(equipmentToRemove);
                    LastModifiedAt = DateTime.UtcNow;
                }
            }

            // ==================== Services Management ====================
            public Result<DepartmentService> AddService(string serviceName, string description, Money price)
            {
                var serviceResult = DepartmentService.Create(Id, serviceName, description, price);
                if (!serviceResult.IsSuccess)
                    return Result<DepartmentService>.Fail(serviceResult.Errors);

                _services.Add(serviceResult.Data);
                LastModifiedAt = DateTime.UtcNow;

                return Result<DepartmentService>.Ok(serviceResult.Data);
            }

            public void RemoveService(Guid serviceId)
            {
                var serviceToRemove = _services.FirstOrDefault(s => s.Id == serviceId);
                if (serviceToRemove != null)
                {
                    _services.Remove(serviceToRemove);
                    LastModifiedAt = DateTime.UtcNow;
                }
            }

            // ==================== Nurses Management ====================
            public void AddNurse(Nurse nurse)
            {
                if (nurse != null && !_nurses.Any(n => n.Id == nurse.Id))
                {
                    _nurses.Add(nurse);
                    LastModifiedAt = DateTime.UtcNow;
                }
            }

        public Result RemoveNurse(Guid nurseId)
        {
            var nurseToRemove = _nurses.FirstOrDefault(n => n.Id == nurseId);
            if (nurseToRemove == null)
                return Result.Fail(Error.NotFound("Nurse.NotFound", "The nurse was not found."));

            _nurses.Remove(nurseToRemove);
            LastModifiedAt = DateTime.UtcNow;
            return Result.ok();
        }

        // ==================== On-Call Schedules Management ====================
        public void AddSchedule(OnCallSchedule schedule)
            {
                if (schedule != null && !_schedules.Any(s => s.Id == schedule.Id))
                {
                    _schedules.Add(schedule);
                    LastModifiedAt = DateTime.UtcNow;
                }
            }

            public void RemoveSchedule(Guid scheduleId)
            {
                var scheduleToRemove = _schedules.FirstOrDefault(s => s.Id == scheduleId);
                if (scheduleToRemove != null)
                {
                    _schedules.Remove(scheduleToRemove);
                    LastModifiedAt = DateTime.UtcNow;
                }
            }
        }
    }


