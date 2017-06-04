// JavaScript source code
(function($) {
    $.fn.mazeBoard = function(mazeData,
        startRow,
        startCol,
        exitRow,
        exitCol,
        playerImage,
        exitImage,
        wallImg,
        isEnabled) {
        class mazeBoard {
            constructor(mazeData, startRow, startCol, exitRow, exitCol, playerImage, exitImage, wallImg, isEnabled, canvas) {
                this._data = mazeData;
                this._startRow = startRow;
                this._startCol = startCol;
                this._exitRow = exitRow;
                this._exitCol = exitCol;
                this._playerRow = startRow;
                this._playerCol = startCol;
                this._playerImg = playerImage;
                this._exitImg = exitImage;
                this._wallImg = wallImg;
                this._isEnabled = isEnabled;
                //this._move = callback;
                this._canvas = canvas;
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
                            ctx.drawImage(this._playerImg, width * j, height * i, width, height);
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


            move(direction) {
                var ctx = this._canvas.getContext("2d");
                var rows = this._data.length;
                var cols = this._data[0].length;
                var width = this._canvas.width / cols;
                var height = this._canvas.height / rows;
                if (this.isValidMove(direction)) {
                    switch (direction) {
                    case "up":
                        ctx.fillRect(width * this._playerCol, height * this._playerRow, width, height);
                        this._playerRow -= 1;
                        ctx.drawImage(this._playerImg,
                            width * this._playerCol,
                            height * this._playerRow,
                            width,
                            height);
                        break;
                    case "down":
                        ctx.fillRect(width * this._playerCol, height * this._playerRow, width, height);
                        this._playerRow += 1;
                        ctx.drawImage(this._playerImg,
                            width * this._playerCol,
                            height * this._playerRow,
                            width,
                            height);
                        break;
                    case "right":
                        ctx.fillRect(width * this._playerCol, height * this._playerRow, width, height);
                        this._playerCol += 1;
                        ctx.drawImage(this._playerImg,
                            width * this._playerCol,
                            height * this._playerRow,
                            width,
                            height);
                        break;
                    case "left":
                        ctx.fillRect(width * this._playerCol, height * this._playerRow, width, height);
                        this._playerCol -= 1;
                        ctx.drawImage(this._playerImg,
                            width * this._playerCol,
                            height * this._playerRow,
                            width,
                            height);
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
            }
        }

        return new mazeBoard(mazeData,
            startRow,
            startCol,
            exitRow,
            exitCol,
            playerImage,
            exitImage,
            wallImg,
            isEnabled,
            $(this)[0]);
    }
})(jQuery);