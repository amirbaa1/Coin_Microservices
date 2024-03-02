var connection = new SignalR.HubConnectionBuilder().withUrl("/coinhub").build();

connection.on("UpdateCoin", (data) => {
    // Handle the updated coin data here
    console.log("Received updated coin data:", data);
});

connection.start().then(() => {
    console.log("Connected to CoinHub");
}).catch((err) => {
    console.error("Error connecting to CoinHub:", err);
});