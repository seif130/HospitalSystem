using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Contract;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Enums;
using HospitalSystem.Domain.ValueObjects;
using HospitalSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Modules.SchedlingInfrastructure.Repositories
{

    public sealed class AppointmentRepository: Repository<Appointment, AppointmentId>, IAppointmentRepository
    {
        public AppointmentRepository(
            SchedulingDbContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<Appointment>> GetByDoctorAsync(
            DoctorId doctorId,
            DateRange period,
            CancellationToken ct = default)
        {
            return await DbSet
                .Where(x =>
                    x.DoctorId == doctorId &&
                    x.ScheduledPeriod.Start <
                        (period.End ?? DateTime.MaxValue) &&
                    period.Start <
                        (x.ScheduledPeriod.End ?? DateTime.MaxValue) &&
                    x.Status != AppointmentStatus.Cancelled &&
                    x.Status != AppointmentStatus.NoShow)
                .OrderBy(x => x.ScheduledPeriod.Start)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Appointment>> GetByPatientAsync(
            PatientId patientId,
            DateRange period,
            CancellationToken ct = default)
        {
            return await DbSet
                .Where(x =>
                    x.PatientId == patientId &&
                    x.ScheduledPeriod.Start <
                        (period.End ?? DateTime.MaxValue) &&
                    period.Start <
                        (x.ScheduledPeriod.End ?? DateTime.MaxValue) &&
                    x.Status != AppointmentStatus.Cancelled &&
                    x.Status != AppointmentStatus.NoShow)
                .OrderBy(x => x.ScheduledPeriod.Start)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Appointment>> GetByClinicRoomAsync(
            ClinicRoomId clinicRoomId,
            DateRange period,
            CancellationToken ct = default)
        {
            return await DbSet
                .Where(x =>
                    x.ClinicRoomId == clinicRoomId &&
                    x.ScheduledPeriod.Start <
                        (period.End ?? DateTime.MaxValue) &&
                    period.Start <
                        (x.ScheduledPeriod.End ?? DateTime.MaxValue) &&
                    x.Status != AppointmentStatus.Cancelled &&
                    x.Status != AppointmentStatus.NoShow)
                .OrderBy(x => x.ScheduledPeriod.Start)
                .ToListAsync(ct);
        }

        public Task<bool> HasDoctorConflictAsync(
            DoctorId doctorId,
            DateRange period,
            CancellationToken ct = default)
        {
            return DbSet.AnyAsync(
                x =>
                    x.DoctorId == doctorId &&
                    x.Status != AppointmentStatus.Cancelled &&
                    x.Status != AppointmentStatus.NoShow &&
                    x.ScheduledPeriod.Start <
                        (period.End ?? DateTime.MaxValue) &&
                    period.Start <
                        (x.ScheduledPeriod.End ?? DateTime.MaxValue),
                ct);
        }

        public Task<bool> HasPatientConflictAsync(
            PatientId patientId,
            DateRange period,
            CancellationToken ct = default)
        {
            return DbSet.AnyAsync(
                x =>
                    x.PatientId == patientId &&
                    x.Status != AppointmentStatus.Cancelled &&
                    x.Status != AppointmentStatus.NoShow &&
                    x.ScheduledPeriod.Start <
                        (period.End ?? DateTime.MaxValue) &&
                    period.Start <
                        (x.ScheduledPeriod.End ?? DateTime.MaxValue),
                ct);
        }

        public Task<bool> HasClinicRoomConflictAsync(
            ClinicRoomId clinicRoomId,
            DateRange period,
            CancellationToken ct = default)
        {
            return DbSet.AnyAsync(
                x =>
                    x.ClinicRoomId == clinicRoomId &&
                    x.Status != AppointmentStatus.Cancelled &&
                    x.Status != AppointmentStatus.NoShow &&
                    x.ScheduledPeriod.Start <
                        (period.End ?? DateTime.MaxValue) &&
                    period.Start <
                        (x.ScheduledPeriod.End ?? DateTime.MaxValue),
                ct);
        }

        public Task<bool> HasDoctorConflictAsync(
            DoctorId doctorId,
            DateRange period,
            AppointmentId excludingAppointmentId,
            CancellationToken ct = default)
        {
            return DbSet.AnyAsync(
                x =>
                    x.DoctorId == doctorId &&
                    x.Id != excludingAppointmentId &&
                    x.Status != AppointmentStatus.Cancelled &&
                    x.Status != AppointmentStatus.NoShow &&
                    x.ScheduledPeriod.Start <
                        (period.End ?? DateTime.MaxValue) &&
                    period.Start <
                        (x.ScheduledPeriod.End ?? DateTime.MaxValue),
                ct);
        }

        public Task<bool> HasPatientConflictAsync(
            PatientId patientId,
            DateRange period,
            AppointmentId excludingAppointmentId,
            CancellationToken ct = default)
        {
            return DbSet.AnyAsync(
                x =>
                    x.PatientId == patientId &&
                    x.Id != excludingAppointmentId &&
                    x.Status != AppointmentStatus.Cancelled &&
                    x.Status != AppointmentStatus.NoShow &&
                    x.ScheduledPeriod.Start <
                        (period.End ?? DateTime.MaxValue) &&
                    period.Start <
                        (x.ScheduledPeriod.End ?? DateTime.MaxValue),
                ct);
        }

        public Task<bool> HasClinicRoomConflictAsync(
            ClinicRoomId clinicRoomId,
            DateRange period,
            AppointmentId excludingAppointmentId,
            CancellationToken ct = default)
        {
            return DbSet.AnyAsync(
                x =>
                    x.ClinicRoomId == clinicRoomId &&
                    x.Id != excludingAppointmentId &&
                    x.Status != AppointmentStatus.Cancelled &&
                    x.Status != AppointmentStatus.NoShow &&
                    x.ScheduledPeriod.Start <
                        (period.End ?? DateTime.MaxValue) &&
                    period.Start <
                        (x.ScheduledPeriod.End ?? DateTime.MaxValue),
                ct);
        }
    }


}
