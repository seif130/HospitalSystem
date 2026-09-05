using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Enum;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Events;
using HospitalSystem.Domain.Primitives;

namespace HospitalSystem.Domain.Modules.Procurement.Vendors;

public sealed class Vendor : AggregateRoot<VendorId>
{
    public string Name { get; private set; } = null!;
    public string NormalizedName { get; private set; } = null!;
    public string? ContactEmail { get; private set; }
    public string? ContactPhone { get; private set; }
    public VendorStatus Status { get; private set; }

    private Vendor() { }

    private Vendor(VendorId id, string name, string? contactEmail, string? contactPhone)
        : base(id)
    {
        Name = name;
        NormalizedName = NormalizeName(name);
        ContactEmail = Normalize(contactEmail);
        ContactPhone = Normalize(contactPhone);
        Status = VendorStatus.Active;
    }

    public static Vendor Create(string name, string? contactEmail = null, string? contactPhone = null)
    {
        var normalizedName = NormalizeRequired(name, "Vendor name is required.");
        return new Vendor(VendorId.New(), normalizedName, contactEmail, contactPhone);
    }

    public void Rename(string name)
    {
        var normalizedName = NormalizeRequired(name, "Vendor name is required.");
        Name = normalizedName;
        NormalizedName = NormalizeName(normalizedName);
    }

    public void UpdateContact(string? contactEmail, string? contactPhone)
    {
        ContactEmail = Normalize(contactEmail);
        ContactPhone = Normalize(contactPhone);
    }

    public void Deactivate()
    {
        if (Status == VendorStatus.Inactive)
            throw new DomainException("Vendor is already inactive.");

        Status = VendorStatus.Inactive;
        AddDomainEvent(new VendorDeactivatedDomainEvent(Id));
    }

    public void Activate()
    {
        if (Status == VendorStatus.Active)
            return;

        Status = VendorStatus.Active;
    }

    private static string NormalizeName(string value) => value.Trim().ToUpperInvariant();

    private static string NormalizeRequired(string? value, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(message);
        return value.Trim();
    }

    private static string? Normalize(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
