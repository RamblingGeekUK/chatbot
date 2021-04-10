"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

var messageBox = $("#message-box");
var messages = [];

var height = 80;
var startY = document.body.offsetHeight - height - 5;
var total = messages.length;
connection.on("receivetwitchmessage", function (user, message) {

    total++;
    addMessage(user, message, total);
});

function addMessage(chatUser, chatMsg, total) {
    console.log(chatMsg)
   
    console.log("get total length "  + total)
    for (var i = 0; i < total; i++) {

        var msg = messages[i];
        //var pos = startY - ((i + 1) * height);
        var pos = -((i + 1) * height);

        TweenLite.to(msg, 0.5, { y: pos });
    }

    //var newMessage = $("<div class='message'>" + chatUser + "::" + chatMsg + "</div>");
    var newMessage = $('<div class="message">',
    newMessage = '<div class="toast" role="alert" aria-live="assertive" aria-atomic="true">',
    newMessage += '<div class="toast-header">',
    newMessage += '<img src="..." class="rounded mr-2" alt="...">',
    newMessage += '<strong class="mr-auto">Bootstrap</strong>',
    newMessage += '<small class="text-muted">just now</small>',
    newMessage += '<button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">',
    newMessage += '<span aria-hidden="true">&times;</span>',
    newMessage += '</div>',
    newMessage += '<div class="toast-body">',
    newMessage += 'See? Just like this.',
    newMessage += '</div>',
    newMessage += '</div>',
    newMessage += '</div>');

    messageBox.append(newMessage);
    messages.unshift(newMessage);
    console.log(newMessage);
   
    console.log("after append " + total)


    TweenLite.fromTo(newMessage, 0.5, {
        y: height * 2
    }, {
        y: 0,
        autoAlpha: 0.8
    });
}



connection.start().then(function () {
    $("#message-box")
}).catch(function (err) {
    return console.error(err.tostring());
});