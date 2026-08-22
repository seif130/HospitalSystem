using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Appointments.Contract
{
    public interface IAppointmentRepository: IRepository<Appointment, AppointmentId>
    {
        Task<IReadOnlyList<Appointment>> GetByDoctorAsync(
            DoctorId doctorId,DateRange period,CancellationToken ct = default);

        Task<IReadOnlyList<Appointment>> GetByPatientAsync(
            PatientId patientId,DateRange period, CancellationToken ct = default);

        Task<IReadOnlyList<Appointment>> GetByClinicRoomAsync(
            ClinicRoomId clinicRoomId,DateRange period,CancellationToken ct = default);

        Task<bool> HasDoctorConflictAsync(
            DoctorId doctorId,DateRange period,CancellationToken ct = default);

        Task<bool> HasPatientConflictAsync(
            PatientId patientId,DateRange period,CancellationToken ct = default);

        Task<bool> HasClinicRoomConflictAsync(
            ClinicRoomId clinicRoomId,DateRange period,CancellationToken ct = default);

        Task<bool> HasDoctorConflictAsync(DoctorId doctorId,DateRange period,
            AppointmentId excludingAppointmentId, CancellationToken ct = default);

        Task<bool> HasPatientConflictAsync(PatientId patientId,DateRange period,
            AppointmentId excludingAppointmentId,CancellationToken ct = default);

        Task<bool> HasClinicRoomConflictAsync(ClinicRoomId clinicRoomId,DateRange period,
            AppointmentId excludingAppointmentId,CancellationToken ct = default);
    }



}
