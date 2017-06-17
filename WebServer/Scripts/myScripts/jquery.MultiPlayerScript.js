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
    var GamesUri = "/MultiPlayer";

    function getAllGames() {
        $.getJSON(GamesUri).done(function (data) {
            self.Games(data["games"]);
        });
    }
    // Fetch the initial data
    getAllGames();

    self.addGames = function () {
        var game = {
            Id: $("#Name").val(),
            maze: StartGame()
        };
        $.post(GamesUri, game).done(function (item) { self.Games.push(item); });
    }
};
ko.applyBindings(new ViewModel());

var board;
exit = new Image();
player_left = new Image();
player_right = new Image();
wall = new Image();
exit.src = "../images/goal.png";
player_left.src = '../images/dave_left.png';
player_right.src = '../images/dave_right.png';
wall.src = '../images/wall.png';

function StartGame() {
    $("#mazeCanvas").hide();
    $(".loader").show();
    alert("wait for second player");
    var name = $("#Name").val();
    var rows = $("#Rows").val();
    var cols = $("#Cols").val();
    var username = sessionStorage.username
    var apiUrl = '/MultiPlayer/' + name + "/" + rows + "/" + cols + "/" + username;
    CreateGame(apiUrl);
};

function JoinGame() {
    $("#mazeCanvas").hide();
    $(".loader").show();
    var name = $("#select").val();
    var username = sessionStorage.username
    var apiUrl = '/MultiPlayer/' + name + "/" + username;
    CreateGame(apiUrl);
}

function CreateGame(gameUri) {
    $.ajax({
        method: "GET",
        url: gameUri
    }).done(function (data) {
        var cols = data["Cols"];
        var maze = [];
        var row = [];
        var i = 0;
        var mazeSrl = data["Maze"];
        for (var c of mazeSrl) {
            if (i < cols) {
                row[i] = parseInt(c);
                i++;
            } else {
                maze.push(row);
                row = [parseInt(c)];
                i = 1;
            }
        };
        maze.push(row);
        board = $("#mazeCanvas").mazeBoard(name, maze,
            data["Start"]["Row"],
            data["Start"]["Col"],
            data["End"]["Row"],
            data["End"]["Col"],
            player_right,
            player_left,
            exit,
            wall,
            true);
        otherBoard = $("#otherMazeCanvas").mazeBoard(name, maze,
            data["Start"]["Row"],
            data["Start"]["Col"],
            data["End"]["Row"],
            data["End"]["Col"],
            player_right,
            player_left,
            exit,
            wall,
            true);
        board.drawMaze();
        $(".loader").hide();
        $("#mazeCanvas").show();
        $("#otherMazeCanvas").show();
        document.title = name;
    })
}
