// JavaScript source code
$(document).ready(function () {
    async: true;
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
        window.location.replace("MainPage.html");
        return;
    }
    window.location.replace("LoginPage.html");
});

$("#Register").click(function () {
    var value = $("#Login").text();
    if (value == "Log off") {
        return;
    }
    window.location.replace("RegisterPage.html");
});

$("#MultiPlayer_Game").click(function () {
    var value = $("#Login").text();
    if (value == "Login") {
        alert("For multiplayer game you need to login");
        window.location.replace("LoginPage.html");
        return;
    }
    window.location.replace("MultiPlayerPage.html");
});