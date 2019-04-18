using System;
using System.Collections.Generic;

namespace Models
{
    public class WorkTask
    {
        public string Name { get; set; }
        public List<TeamTask> Teams { get; set; } = new List<TeamTask>();
        public DateTime CreateDate { get; set; }
    }
}
