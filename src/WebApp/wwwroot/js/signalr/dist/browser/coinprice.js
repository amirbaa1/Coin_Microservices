

var connectUrl = new signalR.HubConnectionBuilder().withUrl("/hub/coinmarket", signalR.HttpTransportType.WebSocket).build();


function updateCoine() {
    connectUrl.invoke("UpdateCoin").then((value) => { console.log("update coin"); });
}

function fulfilled() {
    updateCoine();
    setInterval(updateCoine, 1000);

    console.log("connect Time in hub ");
}

function rejected() {
    console.error("Failed to connect to TimeHub");
}

connectUrl.start().then(fulfilled, rejected);