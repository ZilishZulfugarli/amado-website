var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveMessage", function (user, message) {
    // Handle received messages
    // You can update the UI to display the messages
});

connection.start().then(function () {
    // Rest of your connection start logic...
}).catch(function (err) {
    return console.error(err.toString());
});

// Function to send messages
function sendMessage() {
    var user = $("#userName").val();
    var message = $("#userMessage").val();

    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });

    // Clear the message input field
    $("#userMessage").val("");
}

// Rest of your chat.js code...
