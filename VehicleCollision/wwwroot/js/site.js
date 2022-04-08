// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function () {
    const deleteButton = document.querySelector("button#delete");

    deleteButton.addEventListener('click', (e) => {
        confirmDelete = confirm("Are you sure you want to delete this collision?")

        if (confirmDelete) {
            e.preventDefault();
        }
        else {
            return
        }
    })
  
    var button = document.querySelector("#cookieConsent button[data-cookie-string]");
    button.addEventListener("click", function (event) {
        document.cookie = button.dataset.cookieString;
    }, false);
})();

$('#loginModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus')
})

$(document).ready(function () {
    $("#privacyModal").modal('show');
});