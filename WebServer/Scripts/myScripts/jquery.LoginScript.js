// JavaScript source code

var ViewModel = function () {

    var self = this; // make 'this' available to subfunctions or closures
    var UsersUri = '/api/Users/';

    self.checkUser = function () {
        var username = $('#Name').val();
        var password = $('#Password').val();
        $.getJSON(UsersUri + username + "/" + password).done(function (data) {

            var user = data;

            alert("wellcome " + user["Id"]);
            sessionStorage.Connect = 1;
            sessionStorage.username = user["Id"];
            window.location.href = "MainPage.html";
        });
    }
};
ko.applyBindings(new ViewModel());
