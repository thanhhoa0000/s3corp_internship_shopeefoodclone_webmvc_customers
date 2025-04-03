function updateCartItemQuantity(element) {
    let customerId = $(element).attr("customerId");
    let productId = $(element).attr("productId");
    let quantity = +$(element).attr("quantity");

    $.ajax({
        url: "/Cart/AddToCart",
        type: "POST",
        data: {productId: productId, customerId: customerId, quantity: quantity},
        success: function (response) {
            $(".cart-main-section .cart-products-list div .button-section div").replaceWith(response);
        },
        error: function () {
            alert("Error updating quantity");
        }
    });
}
