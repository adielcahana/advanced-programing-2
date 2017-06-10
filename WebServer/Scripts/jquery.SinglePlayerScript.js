﻿$(document).ready(function () {
    loadSettings();
});

function loadSettings() {
    $('#Name').val("name");
    $('#Rows').val(localStorage.Rows);
    $('#Cols').val(localStorage.Cols);
    $('#Algorithm').val(localStorage.Algorithm);
}

var board;
exit = new Image();
player_left = new Image();
player_right = new Image();
wall = new Image();
exit.src = "../images/goal.png";
player_left.src = '../images/dave_left.png';
player_right.src = '../images/dave_right.png';
wall.src = '../images/wall.png';

function startGame() {
    var name = $("#Name").val();
    var rows = $("#Rows").val();
    var cols = $("#Cols").val();
    var apiUrl = '/SinglePlayer/' + name + "/" + rows + "/" + cols;
    $.ajax({
        method: "GET",
        url: apiUrl
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
        document.onkeydown = function (e) {
            switch (e.which) {
            case 37:
                board.makeMove("left");
                break;
            case 38:
                board.makeMove("up");
                break;
            case 39:
                board.makeMove("right");
                break;
            case 40:
                board.makeMove("down");
                break;
            default:
                break;
            }
        }
        board.drawMaze();
        $("title").text = name;
    });
}

function SolveGame() {
    var alg = $("#Algorithm").val();
    var apiUrl = '/SinglePlayer/' + board._name + "/" + alg;
    $.ajax({
        method: "GET",
        url: apiUrl
    }).done(function (data) {
        var solSrl = data["Solution"];
        var solution = [];
        var i = solSrl.length - 1;
        for (var c of solSrl) {
            solution[i] = parseInt(c);
            i--;
        }
        board.reset();
        board.solve(solution);
    });
}

$("#start")[0].onclick = startGame;
$("#solve")[0].onclick = SolveGame;