using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI
{
    public interface ICosmosRepository
    {
        Task CreateTeam(Team team);
        Task CreateWorkTask(WorkTask workTask);
        Task<WorkTask> GetCurrentTask();
        Task<IEnumerable<Team>> GetTeams();
    }
}
