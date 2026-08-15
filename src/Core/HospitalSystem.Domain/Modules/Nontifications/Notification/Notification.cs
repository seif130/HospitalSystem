using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Nontifications.Notification.Enums;
using HospitalSystem.Domain.Modules.Nontifications.NotificationTemplate.Enum;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Nontifications.Notification
{
    public sealed class Notification : AggregateRoot<NotificationId>
    {
        public NotificationTemplateId TemplateId { get; private set; } = null!;
        public string RecipientIdentifier { get; private set; } = null!; // email, phone, or UserId as string
        public NotificationChannel Channel { get; private set; }
        public string RenderedBody { get; private set; } = null!;
        public NotificationStatus Status { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? SentOnUtc { get; private set; }
        public string? FailureReason { get; private set; }

        private Notification() { }

        private Notification(NotificationId id, NotificationTemplateId templateId, string recipientIdentifier,
            NotificationChannel channel, string renderedBody) : base(id)
        {
            TemplateId = templateId;
            RecipientIdentifier = recipientIdentifier;
            Channel = channel;
            RenderedBody = renderedBody;
            Status = NotificationStatus.Pending;
            CreatedOnUtc = DateTime.UtcNow;
        }

        public static Notification Create(NotificationTemplateId templateId, string recipientIdentifier, NotificationChannel channel, string renderedBody)
        {
            if (string.IsNullOrWhiteSpace(recipientIdentifier)) throw new DomainException("Recipient is required.");
            return new Notification(NotificationId.New(), templateId, recipientIdentifier, channel, renderedBody);
        }

        public void MarkSent()
        {
            if (Status != NotificationStatus.Pending) throw new DomainException("Only a pending notification can be marked sent.");
            Status = NotificationStatus.Sent;
            SentOnUtc = DateTime.UtcNow;
        }

        public void MarkFailed(string reason)
        {
            if (string.IsNullOrWhiteSpace(reason)) throw new DomainException("Failure reason is required.");
            Status = NotificationStatus.Failed;
            FailureReason = reason.Trim();
        }

        public void MarkRead()
        {
            if (Status != NotificationStatus.Sent) throw new DomainException("Only a sent notification can be marked read.");
            Status = NotificationStatus.Read;
        }
    }
}
