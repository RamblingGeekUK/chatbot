"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:5001/chathub").build();

connection.on("ReceiveTwitchMessage", function (message, username) {
    //var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    //var encodedMsg = username + " says " + msg;
    //var li = document.createElement("li");
    //li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
    var tot = document.createElement("myToast");
    tot.getElementById("username").innerHTML = username;
    tot.getElementById("message").innerHTML = message;
    document.getElementById("myToast").appendChild(tot);
    $('.toast').toast('show');
   
  
});

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
    //document.getElementById("myToast").toast('show');
}).catch(function (err) {
    return console.error(err.toString());
});
