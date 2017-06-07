$(document).ready(function () {
    loadSettings();
});

function loadSettings() {
    $('#Name').val("name");
    $('#Rows').val(localStorage.Rows);
    $('#Cols').val(localStorage.Cols);
    $('#Algorithm').val(localStorage.Algorithm);
}