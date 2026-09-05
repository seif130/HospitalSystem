using HospitalSystem.Domain;
using HospitalSystem.Domain.Identifiers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HospitalSystem.Infrastructure.Persistence;

internal static class StrongIdValueConverters
{
    public static readonly ValueConverter<DepartmentId, Guid> Department = new(
        id => id.Value,
        value => new DepartmentId(value));

    public static readonly ValueConverter<SpecialtyId, Guid> Specialty = new(
        id => id.Value,
        value => new SpecialtyId(value));

    public static readonly ValueConverter<DoctorId, Guid> Doctor = new(
        id => id.Value,
        value => new DoctorId(value));

    public static readonly ValueConverter<PatientId, Guid> Patient = new(
        id => id.Value,
        value => new PatientId(value));

    public static readonly ValueConverter<ClinicRoomId, Guid> ClinicRoom = new(
        id => id.Value,
        value => new ClinicRoomId(value));

    public static readonly ValueConverter<AppointmentId, Guid> Appointment = new(
        id => id.Value,
        value => new AppointmentId(value));

    public static readonly ValueConverter<WaitlistId, Guid> Waitlist = new(
        id => id.Value,
        value => new WaitlistId(value));

    public static readonly ValueConverter<AppointmentId?, Guid?> NullableAppointment = new(
        id => id.HasValue ? id.Value.Value : null,
        value => value.HasValue ? new AppointmentId(value.Value) : null);
}

internal static class DateTimeUtcValueConverters
{
    public static readonly ValueConverter<DateTime, DateTime> DateTime = new(
        value => value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime(),
        value => System.DateTime.SpecifyKind(value, DateTimeKind.Utc));

    public static readonly ValueConverter<DateTime?, DateTime?> NullableDateTime = new(
        value => value.HasValue
            ? (value.Value.Kind == DateTimeKind.Utc ? value.Value : value.Value.ToUniversalTime())
            : null,
        value => value.HasValue
            ? System.DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)
            : null);
}
