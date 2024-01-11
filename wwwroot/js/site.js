// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function useCurrentDate() {

    var dateObj = new Date();

    // Obter os componentes da data
    var day = ('0' + dateObj.getDate()).slice(-2);
    var month = ('0' + (dateObj.getMonth() + 1)).slice(-2); // Adicionar 1 e garantir dois dígitos
    var year = dateObj.getFullYear();

    var currentDate = `${year}-${month}-${day}`
    var date = document.querySelector("#date");
    date.value = currentDate;

}