function getSubCategories(cateName) {
    const currentLocation = toSnakeCase(document.getElementById("location-dropdown-btn").textContent);
    $.ajax({
        type: 'POST',
        url: `/Home/Index?province=${currentLocation}&categoryName=${encodeURIComponent(cateName)}`,
        success: function (response) {
            var tempDom = $('<div></div>').html(response);
            var newSearchSection = tempDom.find('.search-container').html();
            var newAddressSection = tempDom.find('.home-vertical-list-section').html();
            $('.search-container').html(newSearchSection);
            $('.home-vertical-list-section').html(newAddressSection);
        },
        error: function () {
            console.error("Failed to fetch sub-categories.");
        }
    });
}