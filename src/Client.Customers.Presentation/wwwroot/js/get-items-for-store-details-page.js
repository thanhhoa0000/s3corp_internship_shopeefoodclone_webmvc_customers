$(document).ready(function () {
    let cate = JSON.parse(localStorage.getItem('cate'));
    const menuItems = document.querySelectorAll(".menu .item");
    
    document.querySelector(`.nav-link[code-name='${cate}']`).click();

    menuItems.forEach(link => {
        link.addEventListener("click", function () {
            menuItems.forEach(item => item.classList.remove("active"));
            this.classList.add("active");
        });
    });
});
