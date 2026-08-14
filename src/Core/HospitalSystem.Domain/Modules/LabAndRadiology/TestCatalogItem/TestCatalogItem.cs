using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.LabAndRadiology.TestCatalogItem.Enum;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.LabAndRadiology.TestCatalogItem
{
    public sealed class TestCatalogItem : AggregateRoot<TestCatalogItemId>
    {
        public string Code { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public CatalogItemType Type { get; private set; }
        public Money Price { get; private set; } = null!;
        public TimeSpan TurnaroundTime { get; private set; }
        public bool IsActive { get; private set; } = true;

        private TestCatalogItem() { }

        private TestCatalogItem(TestCatalogItemId id, string code, string name, CatalogItemType type, Money price, TimeSpan turnaroundTime) : base(id)
        {
            Code = code;
            Name = name;
            Type = type;
            Price = price;
            TurnaroundTime = turnaroundTime;
        }

        public static TestCatalogItem Create(string code, string name, CatalogItemType type, Money price, TimeSpan turnaroundTime)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new DomainException("Catalog code is required.");
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Catalog name is required.");
            return new TestCatalogItem(TestCatalogItemId.New(), code.Trim(), name.Trim(), type, price, turnaroundTime);
        }

        public void UpdatePrice(Money newPrice) => Price = newPrice;
        public void Deactivate() => IsActive = false;
        public void Reactivate() => IsActive = true;
    }
}
