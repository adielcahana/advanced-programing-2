// JavaScript source code
(function($) {
    $.fn.mazeBoard = function(name, mazeData,
        startRow,
        startCol,
        exitRow,
        exitCol,
        playerRight,
        layeLeft,
        exitImage,
        wallImg,
        isEnabled) {

        class mazeBoard {
            constructor(name, mazeData, startRow, startCol, exitRow, exitCol, playerRight, playerLeft, exitImage, wallImg, isEnabled, canvas) {
                this._name = name;
                this._data = mazeData;
                this._startRow = startRow;
                this._startCol = startCol;
                this._exitRow = exitRow;
                this._exitCol = exitCol;
                this._playerRow = startRow;
                this._playerCol = startCol;
                this._playerLeft = playerLeft;
                this._playerRight = playerRight;
                this._player = playerRight;
                this._exitImg = exitImage;
                this._wallImg = wallImg;
                this._isEnabled = isEnabled;
                //this._move = callback;
                this._canvas = canvas;
            }

            reset() {
                this._playerRow = this._startRow;
                this._playerCol = this._startCol;
                this.drawMaze();
            }

            drawMaze() {
                var ctx = this._canvas.getContext("2d");
                ctx.strokeStyle = "black";
                var rows = this._data.length;
                var cols = this._data[0].length;
                var width = this._canvas.width / cols;
                var height = this._canvas.height / rows;
                for (var i = 0; i < rows; i++) {
                    for (var j = 0; j < cols; j++) {
                        if (i == this._startRow && j == this._startCol) {
                            ctx.drawImage(this._playerRight, width * j, height * i, width, height);
                        } else if (i == this._exitRow && j == this._exitCol) {
                            ctx.drawImage(this._exitImg, width * j, height * i, width, height);
                        } else if (this._data[i][j] == 1) {
                            ctx.drawImage(this._wallImg, width * j, height * i, width, height);
                        } else if (this._data[i][j] == 0) {
                            ctx.fillRect(width * j, height * i, width, height);
                        }
                    }
                }
            }

            isValidMove(direction) {
                try
                {
                    switch (direction)
                    {
                    case "up":
                        return this._data[this._playerRow - 1][this._playerCol] == 0;
                    case "down":
                        return this._data[this._playerRow + 1][this._playerCol] == 0;
                    case "right":
                        return this._data[this._playerRow][this._playerCol + 1] == 0;
                    case"left":
                        return this._data[this._playerRow][this._playerCol - 1] == 0;
                    default:
                        var e = new Error();
                        e.message = "wrong argument in isValidMove";
                        throw e;
                    }
                }
                //in case the movement is outside the maze
                catch (e)
                {
                    return false;
                }  
            }

            makeMove(direction) {
                if (this._isEnabled == true) {
                    this.move(direction);
                }
            }

            move(direction) {
                var ctx = this._canvas.getContext("2d");
                var rows = this._data.length;
                var cols = this._data[0].length;
                var width = this._canvas.width / cols;
                var height = this._canvas.height / rows;
                switch (direction) {
                case "right":
                    this._player = this._playerRight;
                    break;
                case "left":
                    this._player = this._playerLeft;
                    break;
               default:
                    break;
                }
                if (this.isValidMove(direction)) {
                    switch (direction) {
                    case "up":
                        ctx.fillRect(width * this._playerCol, height * this._playerRow, width, height);
                        this._playerRow -= 1;
                        break;
                    case "down":
                        ctx.fillRect(width * this._playerCol, height * this._playerRow, width, height);
                        this._playerRow += 1;
                        break;
                    case "right":
                        ctx.fillRect(width * this._playerCol, height * this._playerRow, width, height);
                        this._playerCol += 1;
                        break;
                    case "left":
                        ctx.fillRect(width * this._playerCol, height * this._playerRow, width, height);
                        this._playerCol -= 1;
                        break;
                        default:
                            var e = new Error();
                            e.message = "wrong argument in move";
                            throw e;
                    }
                    if (this._playerRow != this._exitRow || this._playerCol != this._exitCol) {
                        ctx.drawImage(this._exitImg,
                            width * this._exitCol,
                            height * this._exitRow,
                            width,
                            height);
                    }
                }
                ctx.drawImage(this._player,
                    width * this._playerCol,
                    height * this._playerRow,
                    width,
                    height);
            }

            solve(solution) {
                var timer;
                this._isEnabled = false;
                var board = this;
                var i = 0;
                function stage() {
                    switch (solution[i]) {
                        case 0: //left 
                            board.move("left");
                            break;
                        case 1:
                            board.move("right");
                            break;
                        case 2:
                            board.move("up");
                            break;
                        case 3:
                            board.move("down");
                            break;
                    }
                    if (i < solution.length) {
                        i++;
                    } else {
                        board._isEnabled = true;
                        clearInterval(timer);
                    }
                }
                timer = setInterval(stage, 500);
            }
        }

        return new mazeBoard(name, mazeData,
            startRow,
            startCol,
            exitRow,
            exitCol,
            playerRight,
            layeLeft,
            exitImage,
            wallImg,
            isEnabled,
            $(this)[0]);
    }
})(jQuery);