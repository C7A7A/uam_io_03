using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchTogetherAPI.Models;

namespace WatchTogetherAPI.Extensions
{
    static class IntervalExtensions
    {
        public static bool Overlaps(this Interval interval1, Interval interval2)
            => interval1.From <= interval2.From
               ? interval1.To >= interval2.From : interval2.To >= interval1.From;
        public static Interval MergeWith(this Interval interval1, Interval interval2)
    => new Interval
    {
        From = new DateTime(Math.Min(interval1.From.Ticks, interval2.From.Ticks)),
        To = new DateTime(Math.Max(interval1.To.Ticks, interval2.To.Ticks))
    };
        public static Interval Intersection(this Interval interval1, Interval interval2)
            => new Interval
            {
                From = new DateTime(Math.Max(interval1.From.Ticks, interval2.From.Ticks)),
                To = new DateTime(Math.Min(interval1.To.Ticks, interval2.To.Ticks))
            };

        public static IEnumerable<Interval> MergeOverlapping(this IEnumerable<Interval> source)
        {
            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    yield break;
                var previousInterval = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    var nextInterval = enumerator.Current;
                    if (!previousInterval.Overlaps(nextInterval))
                    {
                        yield return previousInterval;
                        previousInterval = nextInterval;
                    }
                    else
                    {
                        previousInterval = previousInterval.MergeWith(nextInterval);
                    }
                }
                yield return previousInterval;
            }
        }

        public static IEnumerable<Interval> IntersectionOverlapping(this IEnumerable<Interval> source)
        {

            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    yield break;
                var previousInterval = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    var nextInterval = enumerator.Current;
                    if (!previousInterval.Overlaps(nextInterval))
                    {
                        source = Enumerable.Empty<Interval>();
                        yield break;
                    }
                    else
                    {
                        previousInterval = previousInterval.Intersection(nextInterval);
                    }
                }
                yield return previousInterval;
            }
        }

    }
}
