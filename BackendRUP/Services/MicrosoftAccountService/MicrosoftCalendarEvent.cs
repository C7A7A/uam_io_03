using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTogetherAPI.Services.MicrosoftAccountService
{
    public class MicrosoftCalendarEvent
    {
        public int id { get; set; }
        public string subject { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
    }
}
