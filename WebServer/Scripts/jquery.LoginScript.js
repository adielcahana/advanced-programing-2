// JavaScript source code

var ViewModel = function () {

    var self = this; // make 'this' available to subfunctions or closures
    //self.users = ko.observableArray(); // enables data binding
    self.thisUser = ko.observable();
    var UsersUri = '/api/Users/';
    /*
    function getAllUsers() {
        $.getJSON(UsersUri).done(function (data) {
            self.users(data);
        });
    }
    // Fetch the initial data
    getAllUsers();
    */
    self.checkUser = function () {
        var username = $('#Name').val();
        var password = $('#Password').val();
        $.getJSON(UsersUri + username).done(function (data) {
            var user = data;

            if (password != user["Password"]) {
                alert("wrond username or password");
                return;
            }

            alert("wellcome " + user["Id"]);
            sessionStorage.Connect = 1;
            sessionStorage.username = user["Id"];
            window.location.href = "MainPage.html";
        });
    }
};
ko.applyBindings(new ViewModel());
