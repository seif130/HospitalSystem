using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Documents.Attachment
{
    public sealed class Attachment : BaseEntity<AttachmentId>
    {
        public string FileName { get; private set; } = null!;
        public string FileUrl { get; private set; } = null!;
        public string ContentType { get; private set; } = null!;
        public long SizeInBytes { get; private set; }
        public DateTime UploadedOnUtc { get; private set; }

        private Attachment() { }

        private Attachment(AttachmentId id, string fileName, string fileUrl, string contentType, long sizeInBytes) : base(id)
        {
            FileName = fileName;
            FileUrl = fileUrl;
            ContentType = contentType;
            SizeInBytes = sizeInBytes;
            UploadedOnUtc = DateTime.UtcNow;
        }

        public static Attachment Upload(string fileName, string fileUrl, string contentType, long sizeInBytes)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new DomainException("File name is required.");
            if (string.IsNullOrWhiteSpace(fileUrl)) throw new DomainException("File URL is required.");
            if (sizeInBytes <= 0) throw new DomainException("File size must be greater than zero.");
            return new Attachment(AttachmentId.New(), fileName.Trim(), fileUrl, contentType, sizeInBytes);
        }
    }

}
