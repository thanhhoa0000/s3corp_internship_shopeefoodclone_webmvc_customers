function getItemsForStorePage(province, districts, category, subcategories) {
    $.ajax({
        url: `/Store/Promotions?province=${province}&categoryName=${category}&districtsString=${districts}&subcategoriesString=${subcategories}`,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            let tempDom = $('<div></div>').html(response);
            let newStoresSection = tempDom.find('.store-main-content').html();
            $('.store-main-content').html(newStoresSection);
            $('.store-section-options p').html(tempDom.find('.store-section-options p').html());

            if (subcategories.length > 0) {
                setTimeout(function () {
                    $('.store-category-item input').each(function () {
                        if (subcategories.includes(this.id)) {
                            $(this).prop('checked', true);

                            let filterTag = document.querySelector('.store-section-category-filter-tag');
                            let checkboxes = document.querySelectorAll('.store-category-item input');
                            let checkedCount = Array.from(checkboxes).filter(checkbox => checkbox.checked).length;

                            filterTag.style.display = 'flex';
                            filterTag.querySelector('span').textContent = `(${checkedCount})`;
                        }
                    });
                }, 100);
            }
        }
    });
}