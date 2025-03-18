function getStoresByDistrict(province, district) {
    const activeCate = document.querySelector('.main-nav-item.active')?.getAttribute('code-name');
    
    $.ajax({
        url: `/Home/Index?province=${encodeURIComponent(province)}&district=${encodeURIComponent(district)}&categoryName=${activeCate}`,
        method: 'POST',
        success: function (response) {
            var tempDom = $('<div></div>').html(response);
            var newStoreSection = tempDom.find('.home-stores-list').html();
            $('.home-stores-list').html(newStoreSection);
        },
        error: function () {
            console.error("Failed to fetch stores.");
        }
    })
}
