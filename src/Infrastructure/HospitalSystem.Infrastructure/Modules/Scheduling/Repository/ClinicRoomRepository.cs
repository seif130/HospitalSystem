using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms;
using HospitalSystem.Domain.Modules.Scheduling.IRepository;
using HospitalSystem.Infrastructure.Contexts.DbContextsCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalSystem.Infrastructure.Modules.Scheduling.Repository
{
    internal sealed class ClinicRoomRepository : IClinicRoomRepository
    {
        private readonly SchedulingDbContext _context;

        public ClinicRoomRepository(SchedulingDbContext context) => _context = context;

        public async Task<ClinicRoom?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var roomId = new ClinicRoomId(id);

            return await _context.ClinicRooms
                .FirstOrDefaultAsync(c => c.Id == roomId, cancellationToken);
        }

        public async Task<List<ClinicRoom>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ClinicRooms.ToListAsync(cancellationToken);
        }

        public void Add(ClinicRoom clinicRoom)
        {
            _context.ClinicRooms.Add(clinicRoom);
        }
    }
}
