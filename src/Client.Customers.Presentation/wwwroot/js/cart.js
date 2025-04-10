$(document).ready(function () {
    let cate = JSON.parse(localStorage.getItem('cate'));

    document.querySelectorAll(`.nav-link`).forEach((item) => {
        item.classList.remove("active");
    })

    document.querySelector(`.nav-link[code-name='${cate}']`).classList.add("active");
});

function checkUserIsAuthenticated(element, customerId) {
    $.ajax({
        url: "/Account/IsUserAuthenticated",
        type: "GET",
        success: function (response) {
            if (!response.isAuthenticated) {
                window.location.href = "/Account/Login";
            } else {
                if (customerId) {
                    updateCartItemQuantity(element);
                } else {
                    console.error("CustomerId not found!");
                }
            }
        },
        error: function () {
            console.error("Error checking authentication!");
        }
    });
}

function updateCartItemQuantity(element) {
    let customerId = $(element).attr("customerId");
    let productId = $(element).attr("productId");
    let quantity = +$(element).attr("quantity");

    $.ajax({
        url: `/Cart/AddToCart?productId=${productId}&quantity=${quantity}&customerId=${customerId}`,
        type: "POST",
        success: function (response) {
            if (response.isCartEmpty){
                window.location.href = "/Cart/CartEmpty";
            }
            let itemId = $(element).attr("item-id");
            let parsed = $('<div>').html(response);
            
            $(`div[cart-item-id=${itemId}]`).replaceWith(parsed.find(`div[cart-item-id=${itemId}]`));
            $('.total-price span').replaceWith(parsed.find('span'));
            toastr.success("Cập nhật giỏ hàng thành công!");
        },
        error: function () {
            toastr.error("Đã xảy ra lỗi!");
        }
    });
}

function proceedToOrderInit(customerId) {
    window.location.href = `/Order/OrderInit?customerId=${customerId}`;
}
