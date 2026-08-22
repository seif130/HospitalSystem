using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.ClinicRooms.Contract
{
    public interface IClinicRoomRepository: IRepository<ClinicRoom, ClinicRoomId>
    {
        Task<bool> ExistsByRoomNumberAsync(
            string roomNumber, DepartmentId departmentId,CancellationToken ct = default);

        Task<IReadOnlyList<ClinicRoom>> GetByDepartmentAsync(
            DepartmentId departmentId,CancellationToken ct = default);

        Task<IReadOnlyList<ClinicRoom>> GetAvailableRoomsAsync(
            DepartmentId departmentId,DateRange period, CancellationToken ct = default);
    }

}
