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
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return GetEqualityComponents()
                .SequenceEqual(other.GetEqualityComponents());
        }

        public override bool Equals(object? obj)
            => obj is ValueObject other && Equals(other);

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(0,(hash, value) => HashCode.Combine(hash, value));
        }

        public static bool operator ==( ValueObject? left, ValueObject? right)
            => left is null? right is null: left.Equals(right);

        public static bool operator !=( ValueObject? left, ValueObject? right) => !(left == right);
    }

}
