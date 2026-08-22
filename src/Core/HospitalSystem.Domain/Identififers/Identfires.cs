using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Identifiers;

// Clinical & Inpatient
public sealed record PatientId(Guid Value) : TypedId(Value)
{
    public static PatientId New() => new(Guid.NewGuid());
}

public sealed record NurseId(Guid Value) : TypedId(Value)
{
    public static NurseId New() => new(Guid.NewGuid());
}

public sealed record MedicalRecordId(Guid Value) : TypedId(Value)
{
    public static MedicalRecordId New() => new(Guid.NewGuid());
}

public sealed record AdmissionId(Guid Value) : TypedId(Value)
{
    public static AdmissionId New() => new(Guid.NewGuid());
}

public sealed record SurgeryId(Guid Value) : TypedId(Value)
{
    public static SurgeryId New() => new(Guid.NewGuid());
}

// Scheduling


#region Scheduling

public sealed record DoctorId(Guid Value) : TypedId(Value)
{
    public static DoctorId New() => new(Guid.NewGuid());
}

public sealed record DoctorAvailabilityId(Guid Value) : TypedId(Value)
{
    public static DoctorAvailabilityId New() => new(Guid.NewGuid());
}

public sealed record DoctorScheduleId(Guid Value): TypedId(Value)
{
    public static DoctorScheduleId New() => new(Guid.NewGuid());
}

public sealed record DoctorTimeOffId(Guid Value): TypedId(Value)
{
    public static DoctorTimeOffId New() => new(Guid.NewGuid());
}

public sealed record SpecialtyId(Guid Value) : TypedId(Value)
{
    public static SpecialtyId New() => new(Guid.NewGuid());
}

public sealed record DepartmentId(Guid Value) : TypedId(Value)
{
    public static DepartmentId New() => new(Guid.NewGuid());
}

public sealed record AppointmentId(Guid Value) : TypedId(Value)
{
    public static AppointmentId New() => new(Guid.NewGuid());
}

public sealed record ClinicRoomId(Guid Value) : TypedId(Value)
{
    public static ClinicRoomId New() => new(Guid.NewGuid());
}

public sealed record WaitlistId(Guid Value) : TypedId(Value)
{
    public static WaitlistId New() => new(Guid.NewGuid());
}

#endregion

// Pharmacy & Inventory
public sealed record PrescriptionId(Guid Value) : TypedId(Value)
{
    public static PrescriptionId New() => new(Guid.NewGuid());
}

public sealed record MedicationId(Guid Value) : TypedId(Value)
{
    public static MedicationId New() => new(Guid.NewGuid());
}

public sealed record InventoryItemId(Guid Value) : TypedId(Value)
{
    public static InventoryItemId New() => new(Guid.NewGuid());
}

public sealed record SupplierId(Guid Value) : TypedId(Value)
{
    public static SupplierId New() => new(Guid.NewGuid());
}
