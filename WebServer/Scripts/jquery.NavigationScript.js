// JavaScript source code

$(document).ready(function () {
    if (sessionStorage.Connect == 1) {
        $("#Login").text("Log off");
        $("#Register").text("Hello " + sessionStorage.username + "!");
    }
    else {
        $("#Login").text("Login");
        $("#Register").text("Register");
    }
});

$("#Login").click(function () {
    var value = $("#Login").text();
    if (value == "Log off") {
        sessionStorage.Connect = 0;
        alert("You Log off");
        window.location.href = "MainPage.html";
        return;
    }
    window.location.href = "LoginPage.html";
});

$("#MultiPlayer_Game").click(function (){
    var value = $("#Login").text();
    if (value == "Login") {
        alert("For multiplayer game you need to login");
        window.location.href = "LoginPage.html";
        return;
    }
    window.location.href = "MultiPlayerPage.html";
});