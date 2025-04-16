$(document).ready(function () {
    let cate = JSON.parse(localStorage.getItem('cate'));
    let locationButton = $('#location-dropdown-btn');

    document.querySelectorAll(`.nav-link`).forEach((item) => {
        item.classList.remove("active");
    })

    document.querySelector(`.nav-link[code-name='${cate}']`).classList.add("active");
});

document.addEventListener("cartUpdated", function (event) {
    const customerId = event.detail.customerId;
    
    $.ajax({
        url: `/Store/GetCartPartial?customerId=${customerId}`,
        type: 'GET',
        success: function (response) {
            let parsed = $('<div>').html(response);
            let cart = $('.cart');
            if (cart.length > 0){
                cart.replaceWith(parsed.find('.cart'));
            }
            else {
                $('.store-details-products-section').append(parsed.find('.cart'));
            }
        },
        error: function () {
            toastr.error("Đã xảy ra lỗi!");
        }
    })
})

$(document).on('click', '.button-section div', function () {
    const id = $(this).attr('id');
    const customerId = $(this).attr('customerId');
    const productId = $(this).attr('productId');
    const originalValue = Number($(this).text());

    const $input = $(`<input class="text-center ms-1 me-1" type="text" id="${id}" value="${originalValue}" />`);

    $input.on('blur', function () {
        const newValue = Number($(this).val());
        const difference = newValue - originalValue;
        $(this).replaceWith(`<div class="col-auto" id="${id}" productId="${productId}" customerId="${customerId}">${newValue}</div>`);

        if (difference !== 0) {
            if (window.location.pathname === "/Cart") {
                $.ajax({
                    url: `/Cart/AddToCart?productId=${productId}&quantity=${difference}&customerId=${customerId}`,
                    type: "POST",
                    success: function (response) {
                        if (response.isCartEmpty) {
                            window.location.href = "/Cart/CartEmpty";
                        }
                        let parsed = $('<div>').html(response);
                        $(`div[cart-item-id=${id}]`).replaceWith(parsed.find(`div[cart-item-id=${id}]`));
                        $('.total-price span').text(parsed.find('span').text());
                    },
                    error: function () {
                        toastr.error("Đã xảy ra lỗi!");
                    }
                });
            }
            if (window.location.pathname === "/Store/Details") {
                $.ajax({
                    url: `/Cart/AddToCartInStoreDetails?productId=${productId}&quantity=${difference}&customerId=${customerId}`,
                    type: "POST",
                    success: function (response) {
                        if (response.isCartEmpty) {
                            $('.cart').remove();
                        }
                        let parsed = $('<div>').html(response);
                        $(`div[cart-item-id=${id}]`).replaceWith(parsed.find(`div[cart-item-id=${id}]`));
                        $('.total-price span').text(parsed.find('span').text());
                    },
                    error: function () {
                        toastr.error("Đã xảy ra lỗi!");
                    }
                });
            }
        }
    });

    $input.on("keypress", function (event) {
        if (!/[0-9]/.test(event.key)) {
            event.preventDefault();
        }
    });

    $input.on("input", function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    $(this).replaceWith($input);
    $input.focus();
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

    if (window.location.pathname === "/Cart") {
        $.ajax({
            url: `/Cart/AddToCart?productId=${productId}&quantity=${quantity}&customerId=${customerId}`,
            type: "POST",
            success: function (response) {
                if (response.isCartEmpty) {
                    window.location.href = "/Cart/CartEmpty";
                }
                let itemId = $(element).attr("item-id");
                let parsed = $('<div>').html(response);

                $(`div[cart-item-id=${itemId}]`).replaceWith(parsed.find(`div[cart-item-id=${itemId}]`));
                $('.total-price span').text(parsed.find('span').text());
            },
            error: function () {
                toastr.error("Đã xảy ra lỗi!");
            }
        });
    }
    if (window.location.pathname === "/Store/Details") {
        $.ajax({
            url: `/Cart/AddToCartInStoreDetails?productId=${productId}&quantity=${quantity}&customerId=${customerId}`,
            type: "POST",
            success: function (response) {
                if (response.isCartEmpty) {
                    $('.cart').remove();
                }
                let itemId = $(element).attr("item-id");
                let parsed = $('<div>').html(response);
                
                let item = $(`div[cart-item-id=${itemId}]`);
                
                if (item.length > 0) {
                    item.replaceWith(parsed.find(`div[cart-item-id=${itemId}]`));
                }
                else{
                    $('.items-list').append(parsed.find(`div[cart-item-id=${itemId}]`))
                }
                
                $('.total-price span').text(parsed.find('span').text());

                document.dispatchEvent(new CustomEvent("cartUpdated", {
                    detail: {
                        customerId: customerId,
                    }
                }));
            },
            error: function () {
                toastr.error("Đã xảy ra lỗi!");
            }
        });
    }
}

function proceedToOrderInit(customerId) {
    window.location.href = `/Order/OrderInit?customerId=${customerId}`;
}
