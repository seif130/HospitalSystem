using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.LabAndRadiology.LabOrder.Enums;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.LabAndRadiology.LabOrder
{
    public sealed class LabOrder : AggregateRoot<LabOrderId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public DoctorId OrderingDoctorId { get; private set; } = null!;
        public DateTime OrderedOnUtc { get; private set; }
        public LabOrderStatus Status { get; private set; }

        private readonly List<LabTestLine> _tests = new();
        public IReadOnlyCollection<LabTestLine> Tests => _tests.AsReadOnly();

        private LabOrder() { }

        private LabOrder(LabOrderId id, PatientId patientId, DoctorId orderingDoctorId) : base(id)
        {
            PatientId = patientId;
            OrderingDoctorId = orderingDoctorId;
            OrderedOnUtc = DateTime.UtcNow;
            Status = LabOrderStatus.Requested;
        }

        public static LabOrder Create(PatientId patientId, DoctorId orderingDoctorId, IEnumerable<LabTestLine> tests)
        {
            var order = new LabOrder(LabOrderId.New(), patientId, orderingDoctorId);
            foreach (var test in tests) order.AddTest(test);
            if (order._tests.Count == 0) throw new DomainException("A lab order must include at least one test.");
            return order;
        }

        public void AddTest(LabTestLine test)
        {
            if (Status != LabOrderStatus.Requested) throw new DomainException("Cannot add tests once specimen collection has started.");
            _tests.Add(test);
        }

        public void MarkSpecimenCollected()
        {
            if (Status != LabOrderStatus.Requested) throw new DomainException("Specimen already collected or order is not pending.");
            Status = LabOrderStatus.SpecimenCollected;
        }

        public void StartProcessing()
        {
            if (Status != LabOrderStatus.SpecimenCollected) throw new DomainException("Specimen must be collected before processing.");
            Status = LabOrderStatus.InProgress;
        }

        public void Complete() => Status = Status == LabOrderStatus.InProgress
            ? LabOrderStatus.Completed
            : throw new DomainException("Order must be in progress to complete.");

        public void Cancel()
        {
            if (Status == LabOrderStatus.Completed) throw new DomainException("Cannot cancel a completed order.");
            Status = LabOrderStatus.Cancelled;
        }
    }
}
