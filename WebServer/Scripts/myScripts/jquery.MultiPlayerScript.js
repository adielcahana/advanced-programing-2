$(document).ready(function () {
    loadSettings();
});

function loadSettings() {
    $('#Name').val("name");
    $('#Rows').val(localStorage.Rows);
    $('#Cols').val(localStorage.Cols);
}

var board, otherBoard;
exit = new Image();
player_left = new Image();
player_right = new Image();
wall = new Image();
exit.src = "../images/goal.png";
player_left.src = '../images/dave_left.png';
player_right.src = '../images/dave_right.png';
wall.src = '../images/wall.png';
var id;
var game = $.connection.multiplayerHub;

game.client.newState = function(move) {
    if (id == move["Id"]) {
        board.makeMove(move["Direction"]);
    } else {
        otherBoard.makeMove(move["Direction"]);
    }
};

function MultiplayerVM() {
    var self = this; // make 'this' available to subfunctions or closures
    self.Games = ko.observableArray(); // enables data binding
};

var vm = new MultiplayerVM();

game.client.list = function (data) {
    vm.Games(data["games"]);
};

$.connection.hub.start().done(function () {
    ko.applyBindings(vm);
    // Fetch the initial data
    game.server.createList();
});

function StartGame() {
    id = 0;
    $("#mazeCanvas").hide();
    $(".loader").show();
    alert("wait for second player");
    var name = $("#Name").val();
    var rows = $("#Rows").val();
    var cols = $("#Cols").val();
    game.server.startGame(name, rows, cols);
};

function JoinGame() {
    id = 1;
    $("#mazeCanvas").hide();
    $(".loader").show();
    var name = $("#select").val();
    game.server.joinGame(name);
}

game.client.createGame = function(data) {
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
    board = $("#mazeCanvas").mazeBoard(name,
        maze,
        data["Start"]["Row"],
        data["Start"]["Col"],
        data["End"]["Row"],
        data["End"]["Col"],
        player_right,
        player_left,
        exit,
        wall,
        true);
    otherBoard = $("#otherMazeCanvas").mazeBoard(name,
        maze,
        data["Start"]["Row"],
        data["Start"]["Col"],
        data["End"]["Row"],
        data["End"]["Col"],
        player_right,
        player_left,
        exit,
        wall,
        true);
    document.onkeydown = function (e) {
        switch (e.which) {
        case 37:
            game.server.addMove(data["Name"], "Left");
            break;
        case 38:
            game.server.addMove(data["Name"], "Up");
            break;
        case 39:
            game.server.addMove(data["Name"], "Right");
            break;
        case 40:
            game.server.addMove(data["Name"], "Down");            
            break;
        default:
            break;
        }
    }
    board.drawMaze();
    otherBoard.drawMaze();
    $(".loader").hide();
    $("#mazeCanvas").show();
    $("#otherMazeCanvas").show();
    document.title = name;
};

$("#join")[0].onclick = JoinGame;
$("#start")[0].onclick = StartGame;