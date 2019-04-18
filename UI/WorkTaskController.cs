using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UI
{
    [Route("api/[controller]")]
    public class WorkTaskController : Controller
    {
        readonly ICosmosRepository cosmosRepository;
        public WorkTaskController(ICosmosRepository cosmosRepository)
        {
            this.cosmosRepository = cosmosRepository;
        }
        // GET: api/<controller>
        [HttpGet]
        public WorkTask GetCurrent()
        {
            var WorkTasks = new WorkTask
            {
                Name = "Say hello to your neighbors",
                Teams = new List<TeamTask>
                {
                    new TeamTask
                    {
                        Name = "The awesomes"
                    },
                    new TeamTask
                    {
                        Name = "In need of help",
                        NeedHelp = true,
                        HelpTime = DateTime.Now
                    },
                    new TeamTask
                    {
                        Name = "Amazingly fast",
                        Complete = true,
                        CompletionTime = DateTime.Now
                    }
                }
            };
            return WorkTasks;
        }


        // POST api/<controller>
        [HttpPost]
        public async Task Post([FromBody]string value)
        {
            var worktask = new WorkTask { CreateDate = DateTime.UtcNow, Name = value };
            await cosmosRepository.CreateWorkTask(worktask);
        }

    }
}
