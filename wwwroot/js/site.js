// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function useCurrentDate() {

    var dateObj = new Date();
    var currentDate = dateObj.toISOString().split('T')[0];
    var date = document.querySelector("#date");
    date.value = currentDate;

}