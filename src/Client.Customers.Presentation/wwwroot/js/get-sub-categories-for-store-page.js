function getSubCategoriesListForStorePage(categoryName) {
    $.ajax({
        url: `https://localhost:5001/categories/sub-categories/get-by-cateName`,
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            categoryName: categoryName
        }),
        success: function (response) {
            if (!response || !response.isSuccessful || !Array.isArray(response.body)) {
                console.error("Invalid API response format:", response);
                return;
            }

            var dropdown = $('#store-category-dropdown .row');
            dropdown.empty();

            response.body.forEach(function (subCategory) {
                var subCategoryItem =
                    `<div class="col-4">
                        <div class="form-check store-category-item ps-5">
                            <input class="form-check-input" type="checkbox" id="${subCategory.codeName}" value="${subCategory.name}" />
                            <label class="form-check-label" for="${subCategory.codeName}">${subCategory.name}</label>
                        </div>
                    </div>`
                dropdown.append(subCategoryItem);
            });
        },
        error: function () {
            console.error("Failed to fetch districts.");
        }
    });
}
