function getStoresByDistrict(province, district) {
    const activeCate = document.querySelector('.main-nav-item.active')?.getAttribute('code-name');
    console.log(activeCate)
    
    $.ajax({
        url: `/Home/Index?province=${province}&districtsString=${district}&categoryName=${activeCate}`,
        method: 'POST',
        success: function (response) {
            let tempDom = $('<div></div>').html(response);
            
            $('.home-stores-main').html(tempDom.find('.home-stores-main').html());
        },
        error: function () {
            console.error("Failed to fetch stores.");
        }
    })
}
