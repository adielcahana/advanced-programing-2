$(document).ready(function () {
    if (localStorage.Rows == undefined) {
        localStorage.Rows = 10;
    }
    if(localStorage.Cols == undefined){
        localStorage.Cols = 10;
    }
    if(localStorage.Algorithm == undefined){
        localStorage.Algorithm = 0;
    }
});