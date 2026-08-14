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
            if (other is null || GetType() != other.GetType()) return false;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override bool Equals(object? obj) => Equals(obj as ValueObject);

        public override int GetHashCode() =>
            GetEqualityComponents().Aggregate(1, (hash, obj) => HashCode.Combine(hash, obj));
    }
}
