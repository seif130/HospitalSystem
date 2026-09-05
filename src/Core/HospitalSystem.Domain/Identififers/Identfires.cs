using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Identifiers;

//Admistartion
public sealed record AttendanceId(Guid Value) : TypedId(Value)
{
    public static AttendanceId New() => new(Guid.NewGuid());
}

public sealed record StaffId(Guid Value) : TypedId(Value)
{
    public static StaffId New() => new(Guid.NewGuid());
}

public sealed record EmploymentContractId(Guid Value) : TypedId(Value)
{
    public static EmploymentContractId New() => new(Guid.NewGuid());
}

public sealed record LeaveRequestId(Guid Value) : TypedId(Value)
{
    public static LeaveRequestId New() => new(Guid.NewGuid());
}

public sealed record PayrollId(Guid Value) : TypedId(Value)
{
    public static PayrollId New() => new(Guid.NewGuid());
}

public sealed record RoomBedId(Guid Value) : TypedId(Value)
{
    public static RoomBedId New() => new(Guid.NewGuid());
}

public sealed record SalaryStructureId(Guid Value) : TypedId(Value)
{
    public static SalaryStructureId New() => new(Guid.NewGuid());
}

public sealed record ShiftScheduleId(Guid Value) : TypedId(Value)
{
    public static ShiftScheduleId New() => new(Guid.NewGuid());
}

public sealed record SystemLogId(Guid Value) : TypedId(Value)
{
    public static SystemLogId New() => new(Guid.NewGuid());
}

public sealed record WardId(Guid Value) : TypedId(Value)
{
    public static WardId New() => new(Guid.NewGuid());
}


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

public sealed record DischargeSummaryId(Guid Value): TypedId(Value)
{
    public static DischargeSummaryId New() => new(Guid.NewGuid());
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
public sealed record BatchId (Guid Value) : TypedId(Value)
{
    public static BatchId New() => new(Guid.NewGuid());
}


public sealed record StockTransactionId(Guid Value) : TypedId(Value)
{
    public static StockTransactionId New() => new(Guid.NewGuid());
}

//assets

public sealed record MedicalEquipmentId(Guid Value) : TypedId(Value)
{
    public static MedicalEquipmentId New() => new(Guid.NewGuid());
}
//blood bank
public sealed record BloodDonorId(Guid Value) : TypedId(Value)
{
    public static BloodDonorId New() => new(Guid.NewGuid());
}
public sealed record BloodRequestId(Guid Value) : TypedId(Value)
{
    public static BloodRequestId New() => new(Guid.NewGuid());
}

public sealed record BloodTransfusionId (Guid Value) : TypedId(Value)
{
    public static BloodTransfusionId New() => new(Guid.NewGuid());
}

public sealed record BloodUnitId (Guid Value) : TypedId(Value)
{
    public static BloodUnitId New() => new(Guid.NewGuid());
}

//notifcation

public sealed record NotificationId(Guid Value) : TypedId(Value)
{
    public static NotificationId New() => new(Guid.NewGuid());
}

public sealed record NotificationTemplateId(Guid Value) : TypedId(Value)
{
    public static NotificationTemplateId New() => new(Guid.NewGuid());
}
//multifacilty

public sealed record FacilityId(Guid Value) : TypedId(Value)
{
    public static FacilityId New() => new(Guid.NewGuid());
}

public sealed record FacilityTransferRequestId(Guid Value) : TypedId(Value)
{
    public static FacilityTransferRequestId New() => new(Guid.NewGuid());
}

//laband radiology

public sealed record LabOrderId(Guid Value) : TypedId(Value)
{
    public static LabOrderId New() => new(Guid.NewGuid());
}

public sealed record LabResultId(Guid Value) : TypedId(Value)
{
    public static LabResultId New() => new(Guid.NewGuid());
}

public sealed record RadiologyOrderId(Guid Value) : TypedId(Value)
{
    public static RadiologyOrderId New() => new(Guid.NewGuid());
}
public sealed record RadiologyReportId(Guid Value) : TypedId(Value)
{
    public static RadiologyReportId New() => new(Guid.NewGuid());
}

public sealed record SpecimenId(Guid Value) : TypedId(Value)
{
    public static SpecimenId New() => new(Guid.NewGuid());
}
public sealed record TestCatalogItemId(Guid Value) : TypedId(Value)
{
    public static TestCatalogItemId New() => new(Guid.NewGuid());
}
//identity

public sealed record PermissionId(Guid Value) : TypedId(Value)
{
    public static PermissionId New() => new(Guid.NewGuid());
}
public sealed record RoleId(Guid Value) : TypedId(Value)
{
    public static RoleId New() => new(Guid.NewGuid());
}
public sealed record UserId(Guid Value) : TypedId(Value)
{
    public static UserId New() => new(Guid.NewGuid());
}

//document

public sealed record AttachmentId(Guid Value) : TypedId(Value)
{
    public static AttachmentId New() => new(Guid.NewGuid());
}
public sealed record DocumentId(Guid Value) : TypedId(Value)
{
    public static DocumentId New() => new(Guid.NewGuid());
}

//compilance

public sealed record AuditLogId(Guid Value) : TypedId(Value)
{
    public static AuditLogId New() => new(Guid.NewGuid());
}

public sealed record ConsentRecordId(Guid Value) : TypedId(Value)
{
    public static ConsentRecordId New() => new(Guid.NewGuid());
}

//emergency

public sealed record AmbulanceId(Guid Value) : TypedId(Value)
{
    public static AmbulanceId New() => new(Guid.NewGuid());
}

public sealed record EmergencyCaseId(Guid Value) : TypedId(Value)
{
    public static EmergencyCaseId New() => new(Guid.NewGuid());
}

public sealed record AmbulanceDispatchId(Guid Value) : TypedId(Value)
{
    public static AmbulanceDispatchId New() => new(Guid.NewGuid());
}

//engagment
public sealed record ComplaintId(Guid Value) : TypedId(Value)
{
    public static ComplaintId New() => new(Guid.NewGuid());
}

public sealed record DoctorReviewId(Guid Value) : TypedId(Value)
{
    public static DoctorReviewId New() => new(Guid.NewGuid());
}
public sealed record PatientFeedbackId(Guid Value) : TypedId(Value)
{
    public static PatientFeedbackId New() => new(Guid.NewGuid());
}
public sealed record ReferralId(Guid Value) : TypedId(Value)
{
    public static ReferralId New() => new(Guid.NewGuid());
}

//finance
public sealed record DiscountAdjustmentId(Guid Value) : TypedId(Value)
{
    public static DiscountAdjustmentId New() => new(Guid.NewGuid());
}

public sealed record InvoiceId(Guid Value) : TypedId(Value)
{
    public static InvoiceId New() => new(Guid.NewGuid());
}

public sealed record PaymentId(Guid Value) : TypedId(Value)
{
    public static PaymentId New() => new(Guid.NewGuid());
}

public sealed record InsuranceClaimId(Guid Value) : TypedId(Value)
{
    public static InsuranceClaimId New() => new(Guid.NewGuid());
}

public sealed record InsurancePolicyId(Guid Value) : TypedId(Value)
{
    public static InsurancePolicyId New() => new(Guid.NewGuid());
}

public sealed record InsuranceProviderId(Guid Value) : TypedId(Value)
{
    public static InsuranceProviderId New() => new(Guid.NewGuid());
}

public sealed record RefundId(Guid Value) : TypedId(Value)
{
    public static RefundId New() => new(Guid.NewGuid());
}

//telemedicine

public sealed record TelemedicinePrescriptionId(Guid Value) : TypedId(Value)
{
    public static TelemedicinePrescriptionId New() => new(Guid.NewGuid());
}

public sealed record TelemedicineSessionId(Guid Value) : TypedId(Value)
{
    public static TelemedicineSessionId New() => new(Guid.NewGuid());
}