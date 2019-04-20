using Newtonsoft.Json;
using System;

namespace Models
{
    public class Team
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
