$(document).ready(function () {
    let cate = JSON.parse(localStorage.getItem('cate'));

    document.querySelectorAll(`.nav-link`).forEach((item) => {
        item.classList.remove("active");
    })

    document.querySelector(`.nav-link[code-name='${cate}']`).classList.add("active");
});

$(function () {
    $.validator.unobtrusive.parse('form');

    $('form input').on('blur', function () {
        $(this).valid();
    });
});
