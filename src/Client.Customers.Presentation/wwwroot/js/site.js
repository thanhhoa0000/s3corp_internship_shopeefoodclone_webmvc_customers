$(document).ready(function () {
    $.ajax({
        url: 'https://localhost:5001/provinces',
        type: 'GET',
        success: function (response) {
            if (!response.isSuccessful || !response.body) {
                console.error("Invalid API response");
                return;
            }

            var dropdown = $('#location-dropdown');
            dropdown.empty();

            var locations = response.body;

            var selectedLocation = localStorage.getItem("selectedLocation") || "Hồ Chí Minh";
            $('#location-dropdown-btn').text(selectedLocation);

            locations.forEach(function (location) {
                dropdown.append(`<li class="location-item"><a class="dropdown-item" onclick="getStores('${toSnakeCase(location.name)}')" data-location="${location.name}">${location.name}</a></li>`);
            });

            $('.dropdown-item').click(function () {
                var selectedLocation = $(this).data('location');
                $('#location-dropdown-btn').text(selectedLocation);

                localStorage.setItem("selectedLocation", selectedLocation);
            });

            setTimeout(function () {
                $('.dropdown-item').each(function () {
                    if ($(this).data('location') === selectedLocation) {
                        $(this).trigger('click');
                        console.log("clicked!");
                    }
                });
            }, 100);
        },
        error: function () {
            console.error("Failed to fetch locations.");
        }
    });
});

function getStores(province) {
    const activeLink = document.querySelector(".main-nav-item.active");
    var cateName = activeLink.getAttribute("code-name");
    
    $.ajax({
        type: 'POST',
        url: `/Home/Index?province=${encodeURIComponent(province)}&categoryName=${encodeURIComponent(cateName)}`,
        success: function (response) {
            var tempDom = $('<div></div>').html(response);
            var newContent = tempDom.find('.home-main-section').html();
            $('.home-main-section').html(newContent);
            console.log(province);
            console.log(newContent);
        },
        error: function () {
            console.error("Failed to fetch stores.");
        }
    });
}

document.addEventListener("DOMContentLoaded", function () {
    const navLinks = document.querySelectorAll(".main-nav .main-nav-item");

    navLinks.forEach(link => {
        link.addEventListener("click", function () {
            navLinks.forEach(item => item.classList.remove("active"));
            this.classList.add("active");
        });
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const cateSections = document.querySelectorAll(".home-footer-item");
    
    cateSections.forEach(section => {
        const title = section.querySelector(".home-footer-item-title");
        const codeName = title.getAttribute("code-name");
        
        $.ajax({
            type: 'GET',
            url: `https://localhost:5001/categories/${codeName}/sub-categories`,
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

function toSnakeCase(str) {
    return str
        .normalize("NFD")
        .replace(/[\u0300-\u036f]/g, '')
        .toLowerCase()
        .replace(/\s+/g, '_');
}

document.addEventListener("DOMContentLoaded", function () {
    const homeFooterItems = Array.from(document.querySelectorAll(".home-footer-item"));
    const totalItems = homeFooterItems.length;
    const columns = 4;
    const perColumn = Math.floor(totalItems / columns);
    const remainder = totalItems % columns;

    // Create new column containers
    const footerContainer = document.querySelector(".footer-main-content");
    footerContainer.innerHTML = "";

    let startIndex = 0;
    for (let i = 0; i < columns; i++) {
        let itemCount = perColumn + (i < remainder ? 1 : 0);
        let colDiv = document.createElement("div");
        colDiv.classList.add("col-md-3");

        for (let j = 0; j < itemCount; j++) {
            colDiv.appendChild(homeFooterItems[startIndex]);
            startIndex++;
        }

        footerContainer.appendChild(colDiv);
    }
});


document.addEventListener("DOMContentLoaded", function () {
    let angle = 0;
    document.querySelector(".rotate-icon").addEventListener("click", function () {
        angle += 360;  // Increase angle on every click
        this.querySelector("i").style.transform = `rotate(${angle}deg)`;
    });
});
