using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.LabAndRadiology.RadiologyOrder.Enums;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.LabAndRadiology.RadiologyOrder
{
    public sealed class RadiologyOrder : AggregateRoot<RadiologyOrderId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public DoctorId OrderingDoctorId { get; private set; } = null!;
        public ImagingModality Modality { get; private set; }
        public string BodyPart { get; private set; } = null!;
        public RadiologyOrderStatus Status { get; private set; }

        private RadiologyOrder() { }

        private RadiologyOrder(RadiologyOrderId id, PatientId patientId, DoctorId orderingDoctorId,
            ImagingModality modality, string bodyPart) : base(id)
        {
            PatientId = patientId;
            OrderingDoctorId = orderingDoctorId;
            Modality = modality;
            BodyPart = bodyPart;
            Status = RadiologyOrderStatus.Requested;
        }

        public static RadiologyOrder Create(PatientId patientId, DoctorId orderingDoctorId, ImagingModality modality, string bodyPart)
        {
            if (string.IsNullOrWhiteSpace(bodyPart)) throw new DomainException("Body part is required.");
            return new RadiologyOrder(RadiologyOrderId.New(), patientId, orderingDoctorId, modality, bodyPart.Trim());
        }

        public void Schedule()
        {
            if (Status != RadiologyOrderStatus.Requested) throw new DomainException("Order is not pending scheduling.");
            Status = RadiologyOrderStatus.Scheduled;
        }

        public void MarkImagingCompleted()
        {
            if (Status != RadiologyOrderStatus.Scheduled) throw new DomainException("Imaging must be scheduled first.");
            Status = RadiologyOrderStatus.ImagingCompleted;
        }

        public void MarkReported() => Status = Status == RadiologyOrderStatus.ImagingCompleted
            ? RadiologyOrderStatus.Reported
            : throw new DomainException("Imaging must be completed before reporting.");
    }

}
