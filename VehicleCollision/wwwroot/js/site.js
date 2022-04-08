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
})();

