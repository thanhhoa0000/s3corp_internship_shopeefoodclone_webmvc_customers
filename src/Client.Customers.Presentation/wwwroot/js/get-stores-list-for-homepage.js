function getStores(province, provinceName) {    
    const activeCate = document.querySelector('.main-nav-item.active')?.getAttribute('code-name');

    $.ajax({
        url: `/Home/Index?province=${province}&categoryName=${activeCate}`,
        type: 'POST',
        success: function (response) {
            getDistricts(province);
            var tempDom = $('<div></div>').html(response);
            var newMainSection = tempDom.find('.home-main-section').html();
            var homeMainSection = $('.home-main-section');
            homeMainSection.html(newMainSection);
            homeMainSection.find('#home-search-location-placeholder').text(provinceName);
            homeMainSection.find('.home-promotion-title a').attr('asp-for-province', province);
            homeMainSection.find('.home-promotion-title a').attr('asp-for-categoryName', activeCate);
        },
        error: function () {
            console.error("Failed to fetch stores.");
        }
    });
}
