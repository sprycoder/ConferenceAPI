using System.Collections.Generic;

namespace Conference.API.Model
{
    public class SpeakerResult
    {
        public int SpeakerID { get; set; }

        public string SpeakerName { get; set; }

        public string Href { get; set; }

        public List<LinkResult> Links { get; set; }
    }
}
