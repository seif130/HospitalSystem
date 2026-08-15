using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace HospitalSystem.Domain.Modules.Documents.Document
{
    public sealed class Document : AggregateRoot<DocumentId>
    {
        public DocumentType Type { get; private set; }
        public string SubjectType { get; private set; } = null!; // "Patient", "Staff"
        public Guid SubjectId { get; private set; }
        public string Title { get; private set; } = null!;
        public DocumentStatus Status { get; private set; }
        public string UploadedByStaffId { get; private set; } = null!;

        private readonly List<Attachment> _attachments = new();
        public IReadOnlyCollection<Attachment> Attachments => _attachments.AsReadOnly();

        private Document() { }

        private Document(DocumentId id, DocumentType type, string subjectType, Guid subjectId, string title, string uploadedByStaffId) : base(id)
        {
            Type = type;
            SubjectType = subjectType;
            SubjectId = subjectId;
            Title = title;
            UploadedByStaffId = uploadedByStaffId;
            Status = DocumentStatus.Draft;
        }

        public static Document Create(DocumentType type, string subjectType, Guid subjectId, string title, string uploadedByStaffId)
        {
            if (string.IsNullOrWhiteSpace(subjectType)) throw new DomainException("Subject type is required.");
            if (string.IsNullOrWhiteSpace(title)) throw new DomainException("Document title is required.");
            return new Document(DocumentId.New(), type, subjectType.Trim(), subjectId, title.Trim(), uploadedByStaffId);
        }

        public void AttachFile(string fileName, string fileUrl, string contentType, long sizeInBytes)
        {
            if (Status == DocumentStatus.Archived) throw new DomainException("Cannot attach files to an archived document.");
            _attachments.Add(Attachment.Upload(fileName, fileUrl, contentType, sizeInBytes));
        }

        public void Finalize()
        {
            if (_attachments.Count == 0) throw new DomainException("Cannot finalize a document with no attachments.");
            Status = DocumentStatus.Final;
        }

        public void Archive() => Status = DocumentStatus.Archived;
    }
}
