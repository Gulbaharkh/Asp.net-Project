using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Models
{
    public class EventSpeaker
    {
        public int Id { get; set; }
        public int EventDetailsId { get; set; }
        public EventDetails EventDetails { get; set; }
        public int SpeakerId { get; set; }
        public Speaker Speaker { get; set; }
    }
}
