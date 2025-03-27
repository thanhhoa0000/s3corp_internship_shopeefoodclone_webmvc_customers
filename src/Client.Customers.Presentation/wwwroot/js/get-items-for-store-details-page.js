$(document).ready(function () {
    let cate = JSON.parse(localStorage.getItem('cate'));
    document.querySelector(`.nav-link[code-name='${cate}']`).click();
});
