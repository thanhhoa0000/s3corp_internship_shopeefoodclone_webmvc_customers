document.addEventListener("configLoaded", getProvinces);

function getProvinces() {
    $.ajax({
        url: `${gatewayUrl}/provinces/with-stores-count`,
        type: 'GET',
        success: function (response) {
            if (!response.isSuccessful || !response.body) {
                console.error("Invalid API response");
                return;
            }

            let dropdown = $('#location-dropdown');
            dropdown.empty();

            let locations = response.body;

            let selectedLocation = localStorage.getItem("selectedLocation") ?? "Hồ Chí Minh";
            $('#location-dropdown-btn').text(selectedLocation);

            locations.sort((a, b) => {
                if (a.codeName === "ho_chi_minh") return -1;
                if (b.codeName === "ho_chi_minh") return 1;
                if (a.codeName === "ha_noi") return -1;
                if (b.codeName === "ha_noi") return 1;
                return 0;
            });

            locations.forEach(function (location) {
                let listItem =
                    $(`
                        <li class="location-item">
                            <a class="dropdown-item d-flex justify-content-between" onclick="getStores('${location.code}', '${location.name}')" data-location="${location.name}" province-code="${location.code}">${location.name} <span class="col">${location.storesCount} địa điểm</span></a>
                        </li>
                    `);
                dropdown.append(listItem);
            });

            $('.dropdown-item').click(function () {
                selectedLocation = $(this).data('location');
                let provinceCode = $(this).attr('province-code');
                let dropdownBtn = $('#location-dropdown-btn');

                dropdownBtn.text(selectedLocation);
                dropdownBtn.attr('province-code', provinceCode);

                localStorage.setItem("selectedLocation", selectedLocation);
            });

            if (window.location.pathname === "/"){
                setTimeout(function () {
                    $('.dropdown-item').each(function () {
                        if ($(this).data('location') === selectedLocation) {
                            $(this).trigger('click');
                        }
                    });
                }, 100);
            }
        },
        error: function () {
            console.error("Failed to fetch locations.");
        }
    });
}


function getDistricts(province) {
    $.ajax({
        url: `${gatewayUrl}/districts?province=${province}`,
        type: 'GET',
        success: function (response) {
            if (!response || !response.isSuccessful || !Array.isArray(response.body)) {
                console.error("Invalid API response format:", response);
                return;
            }

            let dropdown = $('#district-dropdown');
            dropdown.empty();

            response.body.forEach(function (district) {
                let districtItem = `<li class="district-item"><a class="dropdown-item" onclick="getStoresByDistrict('${province}', '${district.code}');" data-district="${district.name}">${district.name.trim().split(/\s+/).length === 1 ? "Quận " + district.name : district.name}</a></li>`;
                dropdown.append(districtItem);
            });

            dropdown.on('click', '.dropdown-item', function () {
                let selectedDistrict = String($(this).data('district') || "").trim();
                $('#district-dropdown-btn').text(selectedDistrict.split(/\s+/).length === 1 ? "Quận " + selectedDistrict : selectedDistrict);
                localStorage.setItem("selectedDistrict", selectedDistrict);
            });
        },
        error: function () {
            console.error("Failed to fetch districts.");
        }
    });
}

function getStores(province, provinceName) {
    if (window.location.pathname !== "/"){
        window.location.href = "/";    
    }
    
    const activeCate = document.querySelector('.main-nav-item.active')?.getAttribute('code-name');

    $.ajax({
        url: `/Home/Index?province=${province}&categoryName=${activeCate}`,
        type: 'POST',
        success: function (response) {
            getDistricts(province);

            let tempDom = $('<div></div>').html(response);
            let homeMainSection = $('.home-main-section');

            homeMainSection.html(tempDom.find('.home-main-section').html());
            homeMainSection.find('#home-search-location-placeholder').text(provinceName);
            homeMainSection.find('.home-search-tags-section a').attr('cate-name', activeCate);
            homeMainSection.find('.home-search-tags-section a').attr('province', province);

            document.querySelectorAll('.home-search-tags-section a').forEach(tag => {
                tag.onclick = () => saveSubCategoryToLocalStorage(tag.getAttribute('sub-category'));
            });
        },
        error: function () {
            console.error("Failed to fetch stores.");
        }
    });
}

// Set active state for main navigation bar categories onclick
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
    const backToTopBtn = document.getElementById("back-to-top-btn");

    window.addEventListener("scroll", function () {
        if (window.scrollY > 100) {
            backToTopBtn.style.display = "block";
            setTimeout(() => backToTopBtn.classList.remove("hidden"));
        } else {
            backToTopBtn.classList.add("hidden");
            setTimeout(() => backToTopBtn.style.display = "none");
        }
    });

    backToTopBtn.addEventListener("click", function () {
        window.scrollTo({top: 0, behavior: "smooth"});
    });
});

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

document.addEventListener("DOMContentLoaded", function () {
    const cateSections = document.querySelectorAll(".home-footer-item");

    cateSections.forEach(section => {
        const title = section.querySelector(".home-footer-item-title");
        const codeName = title.getAttribute("code-name");
        document.addEventListener("configLoaded", () => {
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

                    let subCategories = response.body;

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
        });
    })
})

function setCateLocalStorage(cate, subCate) {
    localStorage.setItem('cate', JSON.stringify(cate));
    localStorage.setItem('sub-category', JSON.stringify(subCate));
}

function getSubCategories(cateName) {
    if (window.location.pathname !== "/") {
        window.location.href = "/";
    }
    
    localStorage.setItem('cate', JSON.stringify(cateName));

    const currentLocation = document.getElementById("location-dropdown-btn").getAttribute("province-code");

    $.ajax({
        url: `/Home/Index?province=${currentLocation}&categoryName=${encodeURIComponent(cateName)}`,
        type: 'POST',
        success: function (response) {
            let tempDom = $('<div></div>').html(response);
            let newSearchSection = tempDom.find('.search-container').html();
            let newAddressSection = tempDom.find('.home-vertical-list-section').html();
            let homeMainSection = $('.home-main-section');

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

document.addEventListener("DOMContentLoaded", function () {
    const backToTopBtn = document.getElementById("back-to-top-btn");

    window.addEventListener("scroll", function () {
        if (window.scrollY > 100) {
            backToTopBtn.style.display = "block";
            setTimeout(() => backToTopBtn.classList.remove("hidden"));
        } else {
            backToTopBtn.classList.add("hidden");
            setTimeout(() => backToTopBtn.style.display = "none");
        }
    });

    backToTopBtn.addEventListener("click", function () {
        window.scrollTo({top: 0, behavior: "smooth"});
    });
});
