"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveTwitchMessage", function (user, message) {
    // Main
    var main = document.createElement('div');
    main.className = 'toast fade show';
    main.id = "chatmsg";
    main.setAttribute('data-autohide', true);

    // Header
    var header = document.createElement('div');
    header.className = 'toast-header';
    header.innerHTML = user;
    main.appendChild(header);

    //// Add Close Button to Header
    //var closebut = document.createElement('button');
    //closebut.className = 'm1-2 mb-1 close';
    //closebut.appendChild(header);

    // Body
    var body = document.createElement('div');
    body.className = 'toast-body';
    body.innerHTML = message;
    main.appendChild(body);

    // Append to the DOM
    document.body.appendChild(main);

    // Show Toast
    $("#twitchchat").toast('show');


});

connection.start().then(function () {
    $("#test").toast('show');
}).catch(function (err) {
    return console.error(err.toString());
});