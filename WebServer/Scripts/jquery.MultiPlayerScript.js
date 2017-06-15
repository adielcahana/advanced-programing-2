$(document).ready(function () {
    loadSettings();
});

function loadSettings() {
    $('#Name').val("name");
    $('#Rows').val(localStorage.Rows);
    $('#Cols').val(localStorage.Cols);
}

var ViewModel = function () {
    var self = this; // make 'this' available to subfunctions or closures

    self.Games = ko.observableArray(); // enables data binding
    var GamesUri = "/api/MultiPlayer";

    function getAllGames() {
        $.getJSON(GamesUri).done(function (data) {
            self.Games(data);
        });
    }
    // Fetch the initial data
    getAllGames();

    self.addGames = function () {
        var game = {
            Id: $("#Name").val(),
            maze: CreateGame()
        };
        $.post(GamesUri, game).done(function (item) { self.Games.push(item); });
    }
};
ko.applyBindings(new ViewModel());

function CreateGame() {
    $("#mazeCanvas").hide();
    $(".loader").show();
    var name = $("#Name").val();
    var rows = $("#Rows").val();
    var cols = $("#Cols").val();
    var m;
    alert(rows);
    var apiUrl = '/MultiPlayer/' + name + "/" + rows + "/" + cols;
    $.ajax({
        method: "GET",
        url: apiUrl
    }).done(function (data) {
        alert(data);
        m =  data;
    })
    alert(data);
    return m;
};

