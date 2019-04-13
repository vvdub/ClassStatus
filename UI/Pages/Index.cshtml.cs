using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace test1.Pages
{
    public class IndexModel : PageModel
    {
        public WorkTask WorkTasks { get; set; }
        public void OnGet()
        {
            WorkTasks = new WorkTask
            {
                Name = "Say hello to your neighbors",
                Teams = new List<Team>
                {
                    new Team
                    {
                        Name = "The awesomes"
                    },
                    new Team
                    {
                        Name = "In need of help",
                        NeedHelp = true,
                        HelpTime = DateTime.Now
                    },
                    new Team
                    {
                        Name = "Amazingly fast",
                        Complete = true,
                        CompletionTime = DateTime.Now
                    }
                }
            };
        }
    }
}
