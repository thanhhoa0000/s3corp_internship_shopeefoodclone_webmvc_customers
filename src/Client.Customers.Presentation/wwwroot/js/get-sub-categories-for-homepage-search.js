function getSubCategories(cateName) {
    localStorage.setItem('cate', JSON.stringify(cateName));
    
    const currentLocation = document.getElementById("location-dropdown-btn").getAttribute("province-code");
    $.ajax({
        url: `/Home/Index?province=${currentLocation}&categoryName=${encodeURIComponent(cateName)}`,
        type: 'POST',
        success: function (response) {
            var tempDom = $('<div></div>').html(response);
            var newSearchSection = tempDom.find('.search-container').html();
            var newAddressSection = tempDom.find('.home-vertical-list-section').html();
            var homeMainSection = $('.home-main-section');
            
            $('.search-container').html(newSearchSection);
            $('.home-vertical-list-section').html(newAddressSection);
            $('.home-search-local').find('#home-search-location-placeholder').text(
                $('#location-dropdown-btn').text()
            );
            homeMainSection.find('.home-search-tags-section a').attr('province', currentLocation);
            homeMainSection.find('.home-search-tags-section a').attr('cate-name', cateName);
            
            document.querySelectorAll('.home-search-tags-section a').forEach(tag => {
               tag.onclick = () => saveSubCategoryToLocalStorage(tag.getAttribute('sub-category'));
            });
        },
        error: function () {
            console.error("Failed to fetch sub-categories.");
        }
    });
}

function saveSubCategoryToLocalStorage(subCategory) {
    localStorage.setItem('sub-category', JSON.stringify(subCategory));
    console.log(localStorage.getItem('sub-category'));
}
