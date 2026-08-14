using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Nontifications.NotificationTemplate
{
    public sealed class NotificationTemplate : AggregateRoot<NotificationTemplateId>
    {
        public string Code { get; private set; } = null!; // e.g. "appointment-reminder"
        public NotificationChannel Channel { get; private set; }
        public string Subject { get; private set; } = null!;
        public string BodyTemplate { get; private set; } = null!; // supports {placeholders}
        public bool IsActive { get; private set; } = true;

        private NotificationTemplate() { }

        private NotificationTemplate(NotificationTemplateId id, string code, NotificationChannel channel, string subject, string bodyTemplate) : base(id)
        {
            Code = code;
            Channel = channel;
            Subject = subject;
            BodyTemplate = bodyTemplate;
        }

        public static NotificationTemplate Create(string code, NotificationChannel channel, string subject, string bodyTemplate)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new DomainException("Template code is required.");
            if (string.IsNullOrWhiteSpace(bodyTemplate)) throw new DomainException("Body template is required.");
            return new NotificationTemplate(NotificationTemplateId.New(), code.Trim(), channel, subject.Trim(), bodyTemplate);
        }

        public string Render(IDictionary<string, string> placeholders)
        {
            var rendered = BodyTemplate;
            foreach (var (key, value) in placeholders)
                rendered = rendered.Replace("{" + key + "}", value);
            return rendered;
        }

        public void Deactivate() => IsActive = false;
    }
}
