// Set active state for main navigation bar categories onclick
document.addEventListener("DOMContentLoaded", function () {
    const navLinks = document.querySelectorAll(".main-nav .main-nav-item");

    navLinks.forEach(link => {
        link.addEventListener("click", function () {
            navLinks.forEach(item => item.classList.remove("active"));
            this.classList.add("active");
        });
    });
});

function getItemsForStorePage(province, districts, category, subcategories) {
    $.ajax({
        url: `/Store/Promotions?province=${province}&categoryName=${category}&districtsString=${districts}&subcategoriesString=${subcategories}`,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var tempDom = $('<div></div>').html(response);
            var newStoresSection = tempDom.find('.store-main-content').html();            
            $('.store-main-content').html(newStoresSection);
            $('.store-section-options p').html(tempDom.find('.store-section-options p').html());
        }
    });
}
