using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.LabAndRadiology.ImagingAttachment
{
    public sealed class ImagingAttachment
    {
        public string FileUrl { get; }
        public string FileFormat { get; } // "JPEG", "PNG"
        public DateTime UploadedOnUtc { get; }

        internal ImagingAttachment(string fileUrl, string fileFormat)
        {
            if (string.IsNullOrWhiteSpace(fileUrl)) throw new DomainException("File URL is required.");
            FileUrl = fileUrl;
            FileFormat = fileFormat;
            UploadedOnUtc = DateTime.UtcNow;
        }
    }
}
