using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.ValueObjects
{
    public sealed class DateRange : ValueObject
    {
        public DateTime Start { get; }
        public DateTime? End { get; }

        private DateRange(
            DateTime start,
            DateTime? end)
        {
            Start = start;
            End = end;
        }

        public static DateRange Create(
            DateTime start,
            DateTime? end = null)
        {
            if (end.HasValue && end.Value < start)
                throw new DomainException("End date cannot be before the start date.");

            return new DateRange(start, end);
        }

        public bool IsOpen
            => End is null;

        public bool Overlaps(DateRange other)
        {
            ArgumentNullException.ThrowIfNull(other);

            return Start < (other.End ?? DateTime.MaxValue) && other.Start < (End ?? DateTime.MaxValue);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }
    }

}
