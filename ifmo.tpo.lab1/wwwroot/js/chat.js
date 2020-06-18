"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/broadcast").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveResponse", function (message) {
    document.getElementById("messages").innerHTML = "";
    document.getElementById("errorsList").hidden = false;
    document.getElementById("errorsList").innerHTML = "";
    for (var i = 0; i < message.length; i++) {
        var msg = message[i].replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var li = document.createElement("li");
        li.textContent = msg;
        document.getElementById("errorsList").appendChild(li);
    }
});

connection.on("ReceiveSubscription", function (data) {
    document.getElementById("errorsList").hidden = true;
    document.getElementById("errorsList").innerHTML = "";
    document.getElementById("messages").innerHTML += "<p><a href=\"" + data + "\">" + data +"</a></p>"
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var action = document.getElementById("actionInput").value == "" ? null : document.getElementById("actionInput").value;
    var topic = document.getElementById("topicInput").value == "" ? null : document.getElementById("topicInput").value;
    var errors = document.getElementById("errorsInput").value == "" ? null : document.getElementById("errorsInput").value;
    var interval = document.getElementById("intervalInput").value == "" ? null : document.getElementById("intervalInput").value;
    var format = document.getElementById("formatInput").value == "" ? null : document.getElementById("formatInput").value;
    var order = document.getElementById("orderInput").value == "" ? null : document.getElementById("orderInput").value;

    connection.invoke("GetSubscription", action, topic, errors, interval, format, order);
    /*var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });*/
    event.preventDefault();
});