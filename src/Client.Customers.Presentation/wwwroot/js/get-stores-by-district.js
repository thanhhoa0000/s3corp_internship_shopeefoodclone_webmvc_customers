function getStoresByDistrict(province, district) {
    const activeCate = document.querySelector('.main-nav-item.active')?.getAttribute('code-name');
    console.log(activeCate)
    
    $.ajax({
        url: `/Home/Index?province=${province}&district=${district}&categoryName=${activeCate}`,
        method: 'POST',
        success: function (response) {
            var tempDom = $('<div></div>').html(response);
            var newStoreSection = tempDom.find('.home-stores-main').html();
            $('.home-stores-main').html(newStoreSection);
        },
        error: function () {
            console.error("Failed to fetch stores.");
        }
    })
}
