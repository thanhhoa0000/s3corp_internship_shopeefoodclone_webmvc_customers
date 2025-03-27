function getStores(province, provinceName) {    
    const activeCate = document.querySelector('.main-nav-item.active')?.getAttribute('code-name');

    $.ajax({
        url: `/Home/Index?province=${province}&categoryName=${activeCate}`,
        type: 'POST',
        success: function (response) {
            getDistricts(province);
            
            let tempDom = $('<div></div>').html(response);
            let homeMainSection = $('.home-main-section');
            
            homeMainSection.html(tempDom.find('.home-main-section').html());
            homeMainSection.find('#home-search-location-placeholder').text(provinceName);
            homeMainSection.find('.home-search-tags-section a').attr('cate-name', activeCate);
            homeMainSection.find('.home-search-tags-section a').attr('province', province);
        },
        error: function () {
            console.error("Failed to fetch stores.");
        }
    });
}
