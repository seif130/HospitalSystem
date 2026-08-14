using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Primitives
{
    public abstract record TypedId(Guid Value)
    {
        public override string ToString() => Value.ToString();
    }
}
