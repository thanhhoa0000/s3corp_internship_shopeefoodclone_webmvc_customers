function getStores(province) {
    const activeCate = document.querySelector('.main-nav-item.active')?.getAttribute('code-name');

    $.ajax({
        type: 'POST',
        url: `/Home/Index?province=${encodeURIComponent(province)}&categoryName=${activeCate}`,
        success: function (response) {
            getDistricts(province)
            var tempDom = $('<div></div>').html(response);
            var newMainSection = tempDom.find('.home-main-section').html();
            $('.home-main-section').html(newMainSection);
        },
        error: function () {
            console.error("Failed to fetch stores.");
        }
    });
}
