"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:5001/chathub").build();

//Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;


connection.on("ReceiveTwitchMessage", function (message, username) {
    //var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    //var encodedMsg = username + " says " + msg;
    //var li = document.createElement("li");
    //li.textContent = encodedMsg;
    //document.getElementById("myToast").appendChild(li);
    var toast = document.createElement("toast");
    toast.getElementById("chatusername").innerHTML = username;
    toast.getElementById("chatmessage").innerHTML = msg;
    document.getElementById("myToast").appendChild();
    //document.getElementById("chatusername").innerHTML = username;
    //document.getElementById("chatmessage").innerHTML = msg;
    
    //$('.toast').toast('show');
});

connection.start().then(function () {

}).catch(function (err) {
    return console.error(err.toString());
});
