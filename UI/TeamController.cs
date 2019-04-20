using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UI
{
    [Route("api/[controller]")]
    public class TeamController : Controller
    {
        readonly ICosmosRepository cosmosRepository;
        public TeamController(ICosmosRepository cosmosRepository)
        {
            this.cosmosRepository = cosmosRepository;
        }
        // POST api/<controller>
        [HttpPost]
        public async Task<Guid> Post([FromBody]string value)
        {
            return await cosmosRepository.CreateTeam(new Models.Team { Name = value });
        }

        [HttpPost]
        [Route("done")]
        public async Task Done([FromBody]Guid value)
        {
            var currentTask = await cosmosRepository.GetCurrentTask();
            var currentTeam = currentTask.Teams.Single(x => x.Id == value);
            currentTeam.Complete = true;
            currentTeam.CompletionTime = DateTime.UtcNow;
            currentTeam.NeedHelp = false;
            currentTeam.HelpTime = null;
            await cosmosRepository.UpdateWorkTask(currentTask);
        }
        [HttpPost]
        [Route("help")]
        public async Task NeedHelp([FromBody]Guid value)
        {
            var currentTask = await cosmosRepository.GetCurrentTask();
            var currentTeam = currentTask.Teams.Single(x => x.Id == value);
            currentTeam.Complete = false;
            currentTeam.CompletionTime = null;
            currentTeam.NeedHelp = true;
            currentTeam.HelpTime = DateTime.UtcNow;
            await cosmosRepository.UpdateWorkTask(currentTask);
        }
    }
}
