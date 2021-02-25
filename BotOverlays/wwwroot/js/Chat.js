"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var messages = [];

var height = 80;
var startY = document.body.offsetHeight - height - 5;

connection.on("ReceiveTwitchMessage", function (user, message) {
    console.log(message);
    
    addMessage(messages.length);
});

function addMessage(total) {
    var messageBox = $("#message-box");
    

    console.log(messages.length);

    for (var i = 0; i < total; i++) {

            var msg = messages[i];
            //var pos = startY - ((i + 1) * height);
            var pos = -((i + 1) * height);

            TweenLite.to(msg, 0.5, { y: pos });
        }

    var newMessage = $("<div class='message'>Message " + total + "</div>");

    messageBox.append(newMessage);
    messages.unshift(newMessage);

    TweenLite.fromTo(newMessage, 0.5, {
        y: height * 2
    }, {
        y: 0,
        autoAlpha: 1
    });


}

connection.start().then(function () {
    $("#test").toast('show');
}).catch(function (err) {
    return console.error(err.toString());
});