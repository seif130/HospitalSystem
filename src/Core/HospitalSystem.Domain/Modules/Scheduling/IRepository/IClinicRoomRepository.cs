
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.IRepository
{
    public interface IClinicRoomRepository
    {
        Task<ClinicRoom?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<ClinicRoom>> GetAllAsync(CancellationToken cancellationToken = default);
        void Add(ClinicRoom clinicRoom);
    }
}
