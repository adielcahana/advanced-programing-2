// JavaScript source code

$(document).ready(function () {
    loadSettings();
});

function loadSettings() {
    $('#Rows').val(localStorage.Rows);
    $('#Cols').val(localStorage.Cols);
    $('#Algorithm').val(localStorage.Algorithm);
}

$('#Save').click(function () {
    localStorage.Rows = $('#Rows').val();
    localStorage.Cols = $('#Cols').val();
    localStorage.Algorithm = $('#Algorithm').val();
});