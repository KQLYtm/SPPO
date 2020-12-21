﻿"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub?userId=" + userId).build();
connection.on("sendToUser", (Naslov, Sadrzaj) => {
    var heading = document.createElement("h3");
    heading.textContent = Naslov;

    var p = document.createElement("p");
    p.innerText = Sadrzaj;

    var div = document.createElement("div");
    div.appendChild(heading);
    div.appendChild(p);

    document.getElementById("articleList").appendChild(div);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
}).then(function () {
    document.getElementById("user").innerHTML = "UserId: " + userId;
    connection.invoke('GetConnectionId').then(function (connectionId) {
        document.getElementById('signalRConnectionId').innerHTML = connectionId;
    })
});