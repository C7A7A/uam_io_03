using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTogetherAPI.Models
{
    public class AnnotatedPoint :IComparable<AnnotatedPoint> {
    public DateTime value { get; set; }
    public PointType type { get; set; }




        public int CompareTo([AllowNull] AnnotatedPoint other)
        {
            if (other.value == this.value)
            {
                return this.type < other.type ? -1 : 1;
            }
            else
            {
                return this.value < other.value ? -1 : 1;
            }
        }

     
        public enum PointType
    {
        End, GapEnd, GapStart, Start
    }
}
}
