var ViewModel = function () {

    var self = this;
    self.users = ko.observableArray();
    var UsersUri = '/api/Users/';

    self.addUser = function () {
        var username = $('#Name').val();
        var password = $('#Password').val();
        var verifyPassword = $('#VerifyPassword').val();
        var email = $('#Email').val();
        if (password != verifyPassword) {
            alert("password don't match to verify password");
            return;
        }

        var user = {
            Id: username,
            Password: password,
            Email: email
        };
        $.post(UsersUri, user).done(function (item) {
            self.users.push(item);
            alert("User registered successfully");
            window.location.replace("MainPage.html");
        })
        .fail(function (jqXHR, status, errorThrown) {
            if (errorThrown == "BadRequest") {
                alert('Wrong details');
            }
            else if (errorThrown == "Conflict") {
                alert('Username allredy exist');
            }
            else {
                alert('Failed send request to server');
            }
        })
    }
};
ko.applyBindings(new ViewModel());