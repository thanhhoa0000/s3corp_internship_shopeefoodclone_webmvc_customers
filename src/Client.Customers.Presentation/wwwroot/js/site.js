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

function getItemsForStorePage(province, category) {
    $.ajax({
        url: `/Store/Index?province=${province}&categoryName=${category}`,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var tempDom = $('<div></div>').html(response);
            var newStoresSection = tempDom.find('.store-main-section').html();
            $('.store-main-section').html(newStoresSection);
        }
    });
}
