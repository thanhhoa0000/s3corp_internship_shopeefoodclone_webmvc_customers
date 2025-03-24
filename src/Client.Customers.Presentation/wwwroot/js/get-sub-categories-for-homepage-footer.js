document.addEventListener("DOMContentLoaded", function () {
    const cateSections = document.querySelectorAll(".home-footer-item");

    cateSections.forEach(section => {
        const title = section.querySelector(".home-footer-item-title");
        const codeName = title.getAttribute("code-name");

        $.ajax({
            url: `https://localhost:5001/categories/sub-categories/get-by-cateName`,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                categoryName: codeName
            }),
            success: function (response) {
                const itemList = $(section).find(".home-footer-item-list");
                itemList.empty();

                var subCategories = response.body;

                subCategories.forEach(function (subCategory) {
                    itemList.append(`<a href="/Store" onclick="localStorage.setItem('cate', JSON.stringify('${subCategory.category.codeName}'));">${subCategory.name}</a>`)
                })
            },
            error: function () {
                console.error("Failed to fetch sub-categories.");
            }
        })
    })
})
