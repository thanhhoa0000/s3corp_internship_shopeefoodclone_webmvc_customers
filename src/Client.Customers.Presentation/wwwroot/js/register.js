$(document).ready(function () {
    let cate = JSON.parse(localStorage.getItem('cate'));

    document.querySelectorAll(`.nav-link`).forEach((item) => {
        item.classList.remove("active");
    })

    document.querySelector(`.nav-link[code-name='${cate}']`).classList.add("active");
});

$(document).on("keypress", '#PhoneNumber', function (event) {
    if (!/[0-9]/.test(event.key)) {
        event.preventDefault();
    }
});

$(document).on("input", '#PhoneNumber', function () {
    this.value = this.value.replace(/[^0-9]/g, '');
});

$(function () {
    $.validator.unobtrusive.parse('form');

    $('form input').on('blur', function () {
        $(this).valid();
    });
});
