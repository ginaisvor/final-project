using System;
using System.Collections.Generic;
using System.Text;
using Omu.ValueInjecter;

namespace TechEvent.Service
{
    public static class Extensions
    {
        public static ICollection<TTo> InjectFrom<TFrom, TTo>(this ICollection<TTo> to, IEnumerable<TFrom> from) where TTo : new()
        {
            foreach (var source in from)
            {
                var target = new TTo();
                target.InjectFrom(source);
                to.Add(target);
            }
            return to;
        }
    }
}
