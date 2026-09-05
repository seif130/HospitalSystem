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
        public bool IsOpen => End is null;

        private DateRange(DateTime start, DateTime? end)
        {
            Start = start;
            End = end;
        }

        public static DateRange Create(DateTime start, DateTime? end = null)
        {
            if (start.Kind == DateTimeKind.Local || end?.Kind == DateTimeKind.Local)
                throw new DomainException("DateRange must use UTC or unspecified DateTime values.");

            if (end.HasValue && end.Value <= start)
                throw new DomainException("End date must be greater than the start date.");

            return new DateRange(start, end);
        }

        public bool Contains(DateTime instant)
        {
            if (instant.Kind == DateTimeKind.Local)
                throw new DomainException("DateRange comparisons must use UTC or unspecified DateTime values.");

            return instant >= Start && (!End.HasValue || instant < End.Value);
        }

        public bool Overlaps(DateRange other)
        {
            ArgumentNullException.ThrowIfNull(other);

            return Start < (other.End ?? DateTime.MaxValue)
                && other.Start < (End ?? DateTime.MaxValue);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }

        public override string ToString() =>
            End is null ? $"{Start:u} - Open" : $"{Start:u} - {End:u}";
    }



}
