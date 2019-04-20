using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Models
{
    public class WorkTask
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TeamTask> Teams { get; set; } = new List<TeamTask>();
        public DateTime CreateDate { get; set; }
    }
}
