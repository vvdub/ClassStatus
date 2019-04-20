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
        public async Task<WorkTask> GetCurrent()
        {
            return await cosmosRepository.GetCurrentTask();
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
