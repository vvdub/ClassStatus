var connection = new signalR.HubConnectionBuilder().withUrl(socketUrl + "teamHub").build();

connection.on("TeamAdded", (message) => {
    console.log(message.name);
    if (!model.teams.find(x => x.id === message.id)) {
        model.teams.push(message);
    }
});

connection.on("Working", (message) => {
    console.log(message.name);
    let team = model.teams.find(x => x.id === message.id);
    team.needHelp = false;
    team.complete = false;
    team.helpTime = null;
    team.completionTime = null;
});

connection.on("Help", (message) => {
    console.log(message.name);
    let team = model.teams.find(x => x.id === message.id);
    team.needHelp = true;
    team.complete = false;
    team.helpTime = moment();
    team.completionTime = null;
});

connection.on("Done", (message) => {
    console.log(message.name);
    let team = model.teams.find(x => x.id === message.id);
    team.needHelp = false;
    team.complete = true;
    team.helpTime = null;
    team.completionTime = moment();
});

connection.on("TaskUpdated", (message) => {
    for (let team of model.teams) {
        team.needHelp = false;
        team.complete = false;
        team.helpTime = null;
        team.completionTime = null;
        
    }
    model.name = message;
});

connection.start().then(() => {
    console.log("here");
});