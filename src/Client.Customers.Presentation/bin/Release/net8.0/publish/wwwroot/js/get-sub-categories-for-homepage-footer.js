document.addEventListener("DOMContentLoaded", function () {
    const cateSections = document.querySelectorAll(".home-footer-item");

    cateSections.forEach(section => {
        const title = section.querySelector(".home-footer-item-title");
        const codeName = title.getAttribute("code-name");
        $.ajax({
            url: `${gatewayUrl}/categories/sub-categories/get-by-cateName`,
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
                    itemList.append(`<a href="/Store/Promotions" cate="${subCategory.category.codeName}" sub-cate="${subCategory.codeName}">${subCategory.name}</a>`)
                });

                document.querySelectorAll('.home-footer-item-list a').forEach(item => {
                    item.onclick = () => setCateLocalStorage(item.getAttribute("cate"), item.getAttribute("sub-cate"));
                })
            },
            error: function () {
                console.error("Failed to fetch sub-categories.");
            }
        });
    })
})

function setCateLocalStorage(cate, subCate) {
    localStorage.setItem('cate', JSON.stringify(cate));
    localStorage.setItem('sub-category', JSON.stringify(subCate));
}
