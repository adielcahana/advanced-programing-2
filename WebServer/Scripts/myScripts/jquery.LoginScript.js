// JavaScript source code

var ViewModel = function () {

    var self = this;
    var UsersUri = '/api/Users/';

    self.checkUser = function () {
        var username = $('#Name').val();
        var password = $('#Password').val();
        $.getJSON(UsersUri + username + "/" + password).done(function (data) {

            var user = data;

            alert("welcome " + user["Id"]);
            sessionStorage.Connect = 1;
            sessionStorage.username = user["Id"];
            window.location.replace("MainPage.html");
        })
        .fail(function (jqXHR, status, errorThrown) {
            if (errorThrown == "Not Found") {
                alert('Wrong username or password');
            }
            else {
                alert('Failed send request to server');
            }
        })
    }
};
ko.applyBindings(new ViewModel());
