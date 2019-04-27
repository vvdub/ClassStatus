using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using Models;
using Newtonsoft.Json;

namespace UI
{
    public class CosmosRepository : ICosmosRepository
    {
        private const string DatabaseId = "classstatus";
        private const string TEAMS = "teams";
        private const string WORKTASKS = "worktasks";
        private static DocumentClient _client;
        readonly IOptions<CosmosSettings> settings;

        public CosmosRepository(IOptions<CosmosSettings> settings)
        {
            this.settings = settings;
        }
        private async Task Init()
        {
            if (_client == null)
            {
                _client = new DocumentClient(new Uri(settings.Value.Uri), settings.Value.Key);

                await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = DatabaseId });
                await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseId), new DocumentCollection { Id = TEAMS });
                await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseId), new DocumentCollection { Id = WORKTASKS });
            }
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            await Init();
            var teams = _client.CreateDocumentQuery<Team>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, TEAMS));
            return teams.ToList();
        }

        public async Task<WorkTask> GetCurrentTask()
        {
            await Init();
            try
            {
               
                var workTask = _client.CreateDocumentQuery<WorkTask>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, WORKTASKS))
                    .OrderByDescending(x => x.CreateDate)
                    .Take(1).ToList().FirstOrDefault();

                return workTask;
            }catch(Exception ex)
            {
                return new WorkTask { Name = "Start" };
            }
        }

        public async Task<Guid> CreateTeam(Team team)
        {
            await Init();
            team.Id = Guid.NewGuid();
            await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, TEAMS), team);
            var currentTask = await GetCurrentTask();
            currentTask.Teams.Add(new TeamTask { Name = team.Name, Id = team.Id });
            await _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, WORKTASKS, currentTask.Id.ToString()), currentTask);
            return team.Id;
        }

        public async Task UpdateWorkTask(WorkTask workTask)
        {
            await _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, WORKTASKS, workTask.Id.ToString()), workTask);
        }

        public async Task CreateWorkTask(WorkTask workTask)
        {
            await Init();
            workTask.Id = Guid.NewGuid();
            var teams = _client.CreateDocumentQuery<Team>(UriFactory.CreateDocumentCollectionUri(DatabaseId, TEAMS)).ToList();
            workTask.Teams.AddRange(teams.Select(x => new TeamTask { Name = x.Name, Id = x.Id }));
            await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, WORKTASKS), workTask);
        }
    }
}
