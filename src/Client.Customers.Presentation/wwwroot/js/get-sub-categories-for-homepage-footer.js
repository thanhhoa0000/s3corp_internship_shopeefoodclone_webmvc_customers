document.addEventListener("DOMContentLoaded", function () {
    const cateSections = document.querySelectorAll(".home-footer-item");

    cateSections.forEach(section => {
        const title = section.querySelector(".home-footer-item-title");
        const codeName = title.getAttribute("code-name");

        $.ajax({
            url: `https://localhost:5001/categories/${codeName}/sub-categories`,
            type: 'GET',
            success: function (response) {
                const itemList = $(section).find(".home-footer-item-list");
                itemList.empty();

                var subCategories = response.body;

                subCategories.forEach(function (subCategory) {
                    itemList.append(`<a href="#">${subCategory.name}</a>`)
                })
            },
            error: function () {
                console.error("Failed to fetch sub-categories.");
            }
        })
    })
})
