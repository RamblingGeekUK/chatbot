"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var toastCounter = 0;

connection.on("ReceiveTwitchMessage", function (user, message) {
    CreateToast(user, message);
    //var thisToast = toastCounter - 1;

    //// Make it slide down
    //$(document).find("#chatmsg_" + thisToast).slideDown(600);

    //setTimeout(function () {
    //    $(document).find("#chatmsg_" + thisToast).slideUp(600, function () {   // Slideup callback executes AFTER the slideup effect.
    //        $(this).remove();
    //    });
    //}, 3000);  // 3sec.
});

function CreateToast(user, message) {
    // Main
    var main = document.createElement('div');
    main.className = 'toast';
    main.id = "chatmsg_" + toastCounter;
    main.setAttribute('role', 'alert');
    main.setAttribute('aria-live', 'assertive');
    main.setAttribute('aria-atomic', 'true');
    main.setAttribute('data-autohide', 'true');

    // Header
    var header = document.createElement('div');
    header.className = 'toast-header';
    header.innerHTML = user;
    main.appendChild(header);

    // Body
    var body = document.createElement('div');
    body.className = 'toast-body';
    body.innerHTML = message;
    main.appendChild(body);

    // Append to the DOM
    //document.body.appendChild(main);
    document.getElementById("twitchchat").appendChild(main);

    // Increment toastCounter
    toastCounter++;  

    // Show Toast
    //$("#twitchchat" + toastCounter).toast('show');
  
}

connection.start().then(function () {
    
}).catch(function (err) {
    return console.error(err.toString());
});