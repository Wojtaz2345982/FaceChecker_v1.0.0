// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', (event) => {
    document.getElementById('changeThemeButton').addEventListener('click', function () {
        var currentTheme = localStorage.getItem('theme');
        var newTheme;

        if (currentTheme === 'dark') {
            newTheme = 'light';
        } else {
            newTheme = 'dark';
        }

        var token = document.querySelector('input[name="__RequestVerificationToken"]').value;

        // Ustaw nowy motyw w local storage
        localStorage.setItem('theme', newTheme);

        fetch('/Home/SetTheme', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            },
            body: `theme=${newTheme}&__RequestVerificationToken=${token}`
        })
            .then(response => {
                if (response.ok) {
                    // Przeładowujemy stronę, aby zastosować nowy motyw
                    window.location.reload();
                } else {
                    throw new Error('Problem with the response');
                }
            }).catch(error => {
                console.error('Error:', error);
            });
    });
});