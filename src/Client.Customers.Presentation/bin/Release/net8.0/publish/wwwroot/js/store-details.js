$(document).ready(function () {
    let cate = JSON.parse(localStorage.getItem('cate'));
    const menuItems = document.querySelectorAll(".menu .item");
    
    document.querySelectorAll(`.nav-link`).forEach((item) => {
        item.classList.remove("active");
    })
    
    document.querySelector(`.nav-link[code-name='${cate}']`).classList.add("active");

    menuItems.forEach(link => {
        link.addEventListener("click", function () {
            menuItems.forEach(item => item.classList.remove("active"));
            this.classList.add("active");
        });
    });
    
    $('.product-search input').on('input', function (event) {
        const keyword = $(this).val().trim().toLowerCase().normalize('NFD').replace(/\p{Diacritic}/gu, '');

        $('.product-item').each(function () {
            const productName = $(this).find('.name').text().trim().toLowerCase();

            if (keyword === "" || productName.includes(keyword)) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    })
});
