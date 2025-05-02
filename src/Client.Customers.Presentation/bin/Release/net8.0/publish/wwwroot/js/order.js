$(document).ready(function () {
    let cate = JSON.parse(localStorage.getItem('cate'));

    document.querySelectorAll(`.nav-link`).forEach((item) => {
        item.classList.remove("active");
    })

    document.querySelector(`.nav-link[code-name='${cate}']`).classList.add("active");

    if (window.location.pathname === "/Order/List") {
        getOrdersList($('.orders-list-section').attr('customer-id'));
    }
});

function getOrdersList(customerId) {
    $.ajax({
        url: `/Order/List?customerId=${customerId}`,
        type: 'POST',
        success: function (response) {
            let parsed = $('<div>').html(response);
            $('.orders-list-section').replaceWith(parsed.find('.orders-list-section'));
        },
        error: function () {
            toastr.error("Đã xảy ra lỗi!");
        }
    });
}
