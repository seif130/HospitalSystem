using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Primitives
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        protected abstract IEnumerable<object?> GetEqualityComponents();

        public bool Equals(ValueObject? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null || GetType() != other.GetType()) return false;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override bool Equals(object? obj) => obj is ValueObject other && Equals(other);

        public override int GetHashCode()
        {
            var hash = new HashCode();
            foreach (var component in GetEqualityComponents())
                hash.Add(component);
            return hash.ToHashCode();
        }

        public static bool operator ==(ValueObject? left, ValueObject? right) =>
            Equals(left, right);

        public static bool operator !=(ValueObject? left, ValueObject? right) =>
            !Equals(left, right);
    }

}
