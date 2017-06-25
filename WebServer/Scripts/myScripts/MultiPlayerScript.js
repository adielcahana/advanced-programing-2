$(document).ready(function () {
    loadSettings();
    $("#join")[0].onclick = JoinGame;
    $("#start")[0].onclick = StartGame;
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
    if (board.gameFinished() == true) {
        game.server.finishGame(board._name);
    }
};

game.client.finishGame = function (msg) {
    alert(msg["msg"]);
    window.location.replace("/Pages/MainPage.html");
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
    var name = $("#Name").val();
    var rows = $("#Rows").val();
    var cols = $("#Cols").val();
    var username = sessionStorage.username;
    game.server.startGame(name, rows, cols, username);
};

function JoinGame() {
    id = 1;
    $("#mazeCanvas").hide();
    var name = $("#select").val();
    var username = sessionStorage.username;
    if (name == undefined) {
        alert("You need to choose game");
        return;
    }
    $(".loader").show();
    game.server.joinGame(name, username);
}

function OnKeyPress(e) {
    switch (e.which) {
    case 37:
        game.server.addMove(board._name, "Left");
        break;
    case 38:
        game.server.addMove(board._name, "Up");
        break;
    case 39:
        game.server.addMove(board._name, "Right");
        break;
    case 40:
        game.server.addMove(board._name, "Down");
        break;
    default:
        break;
    }
}

game.client.initGame = function (data) {
    if (data["msg"] != undefined) {
        alert(data["msg"]);
        $(".loader").hide();
        return;
    }
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
    board = $("#mazeCanvas").mazeBoard(data["Name"],
        maze,
        data["Start"]["Row"],
        data["Start"]["Col"],
        data["End"]["Row"],
        data["End"]["Col"],
        player_right,
        player_left,
        exit,
        wall,
        true,
        null);
    otherBoard = $("#otherMazeCanvas").mazeBoard(data["Name"],
        maze,
        data["Start"]["Row"],
        data["Start"]["Col"],
        data["End"]["Row"],
        data["End"]["Col"],
        player_right,
        player_left,
        exit,
        wall,
        true,
        null);
    alert("waiting for second player");
    if (id == 1) {
        document.onkeydown = OnKeyPress;
        board.drawMaze();
        otherBoard.drawMaze();
        $(".loader").hide();
        $("#mazeCanvas").show();
        $("#otherMazeCanvas").show();
        document.title = name;
    }
};

game.client.startGame = function() {
    document.onkeydown = OnKeyPress;
    board.drawMaze();
    otherBoard.drawMaze();
    $(".loader").hide();
    $("#mazeCanvas").show();
    $("#otherMazeCanvas").show();
    document.title = name;
}

