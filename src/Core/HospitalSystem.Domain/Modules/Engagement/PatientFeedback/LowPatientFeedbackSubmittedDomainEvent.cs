using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Engagement.PatientFeedback.PatientFeedback
{
    public sealed record LowPatientFeedbackSubmittedDomainEvent(PatientFeedbackId FeedbackId, PatientId PatientId, int Rating) : DomainEvent;

}
