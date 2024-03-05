

var connectUrl = new signalR.HubConnectionBuilder().withUrl("/hub/coinlive", signalR.HttpTransportType.WebSocket).build();

//function updateTime() {
//    connectUrl.invoke("TimeAsync").then((value) => {
//        var timeSend = document.getElementById("liveTimeWeb");
//        if (value && value.TimesTamp) {
//            timeSend.innerHTML = value.TimesTamp.toString();
//        }
//    }).catch((error) => {
//        console.error("Failed to get time from server", error);
//    });
//}

function updateTime() {
    connectUrl.invoke("TimeAsync").then((value) => {
        var timeSend = document.getElementById("liveTimeWeb");
        if (value) {
            var timestamp = new Date(value);
            timeSend.innerText = timestamp.toLocaleTimeString();
        } else {
            console.error("Received value is null");
        }
    }).catch((error) => {
        console.error("Failed to get time from server", error);
    });
}


function fulfilled() {
    updateTime();
    setInterval(updateTime, 1000);

    console.log("connect Time in hub ");
}

function rejected() {
    console.error("Failed to connect to TimeHub");
}

connectUrl.start().then(fulfilled, rejected);