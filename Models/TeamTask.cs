using Newtonsoft.Json;
using System;

namespace Models
{
    public class TeamTask
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Complete { get; set; } = false;
        public bool NeedHelp { get; set; } = false;
        public DateTime? CompletionTime { get; set; }
        public DateTime? HelpTime { get; set; }
    }
}
