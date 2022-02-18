using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchTogetherAPI.Models;

namespace WatchTogetherAPI.Helpers
{
    public class IntervalHelpers
    {
        public static List<AnnotatedPoint> initQueue(List<Interval> interval, List<Interval> remove)
        {

            List<AnnotatedPoint> queue = new List<AnnotatedPoint>();
            foreach(Interval i in interval)
            {
                queue.Add(new AnnotatedPoint() { value = i.From, type = AnnotatedPoint.PointType.Start });
                queue.Add(new AnnotatedPoint() { value = i.To, type = AnnotatedPoint.PointType.End });
            }
            foreach(Interval i in remove)
            {
                queue.Add(new AnnotatedPoint() { value = i.From, type = AnnotatedPoint.PointType.GapStart });
                queue.Add(new AnnotatedPoint() { value = i.To, type = AnnotatedPoint.PointType.GapEnd });
            }

            // sort the queue
            queue.Sort();

            return queue;
        }

        public static List<Interval> doSweep(List<Interval> interval, List<Interval> remove)
        {
            List<AnnotatedPoint> queue = initQueue(interval, remove);
            List<Interval> result = new List<Interval>();

            // iterate over the queue       
            bool isInterval = false; // isInterval: #Start seen > #End seen
            bool isGap = false;      // isGap:      #GapStart seen > #GapEnd seen
            DateTime intervalStart = DateTime.MinValue;
            foreach(AnnotatedPoint point in queue)
            {
                switch (point.type)
                {
                    case AnnotatedPoint.PointType.Start:
                        if (!isGap)
                        {
                            intervalStart = point.value;
                        }
                        isInterval = true;
                        break;
                    case AnnotatedPoint.PointType.End:
                        if (!isGap)
                        {
                            result.Add(new Interval()
                            {
                                From = intervalStart,
                                To = point.value
                            });
                        }
                        isInterval = false;
                        break;
                    case AnnotatedPoint.PointType.GapStart:
                        if (isInterval)
                        {
                            result.Add(new Interval()
                            {
                                From = intervalStart,
                                To = point.value
                            });
                        }
                        isGap = true;
                        break;
                    case AnnotatedPoint.PointType.GapEnd:
                        if (isInterval)
                        {
                            intervalStart = point.value;
                        }
                        isGap = false;
                        break;
                }
            }

            return result;
        }
    }
}
