var connectUrl = new signalR.HubConnectionBuilder().withUrl("/hub/coinmarket", signalR.HttpTransportType.WebSocket).build();


// function updateCoine() {
//     connectUrl.invoke("ReceiveCoinStatus").then((value) => {
//         console.log("update coin");
//     })
// }

function updateCoine() {
    console.log("function url :")
    connectUrl.invoke("UpdateCoinPrices", function (v) {
        console.log("Updated Prices:", v);
        // Uncomment and use the following code if you want to update the UI
        v.forEach(function (item) {
            var priceElement = $("#price_" + item.symbol);
            console.log("Symbol : ",item.symbol)
            console.log("Price Element:", priceElement);
            if (priceElement.length > 0) {
                priceElement.text(item.price.toFixed(2) + "$");
            }
        });
    });
}

function startConnection() {
    connectUrl.start().then(function () {
        console.log("Connection started");
        fulfilled();
    }).catch(function (err) {
        console.error("Error starting connection: " + err);
        rejected();
    });
}

function fulfilled() {
    updateCoine();
    setInterval(updateCoine, 60000);

    console.log("Update Price coin ");
}

function rejected() {
    console.error("Failed to connect to TimeHub");
}

startConnection();