using System;
using System.Collections.Generic;

namespace Models
{
    public class WorkTask
    {
        public string Name { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
    }

    public class Team
    {
        public string Name { get; set; }
        public bool Complete { get; set; } = false;
        public bool NeedHelp { get; set; } = false;
        public DateTime? CompletionTime { get; set; }
        public DateTime? HelpTime { get; set; }
    }
}
