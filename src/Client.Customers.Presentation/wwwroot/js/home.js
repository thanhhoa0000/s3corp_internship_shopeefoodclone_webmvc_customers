document.addEventListener("scroll", updateSearchSection);
document.querySelector(".home-search-section").addEventListener("mouseenter", updateSearchSection);
window.addEventListener("pageshow", getProvinces);

$(document).ready(function () {
    let cate = JSON.parse(localStorage.getItem('cate'));
    
    if (!cate) {
        cate = "food";
        localStorage.setItem('cate', JSON.stringify("food"));
    }

    document.querySelectorAll(`.main-nav-item`).forEach((item) => {
        item.classList.remove("active");
    })

    $(`.main-nav-item[code-name='${cate}']`).addClass("active");
    
    localStorage.setItem('promotionsPageSize', "9");
    localStorage.setItem('collectionsPageSize', "9");
    localStorage.setItem('storesPageSize', "9");
});

document.addEventListener("scroll", updateSearchSection);
document.querySelector(".home-search-section").addEventListener("mouseenter", updateSearchSection);

function updateSearchSection() {

    const mainSection = document.querySelector(".home-main-section");
    const verticalListSection = document.querySelector('.home-vertical-list-section');
    const searchSection = document.querySelector(".home-search-section");

    if (hasEnoughSpacing()) {
        if (mainSection.getBoundingClientRect().bottom <= window.innerHeight) {
            let newTop = verticalListSection.offsetHeight - searchSection.offsetHeight - 205 + "px";
            searchSection.style.setProperty("position", "absolute", "important");
            searchSection.style.setProperty("top", newTop, "important");
            searchSection.style.setProperty("left", "20px", "important");
        } else {
            searchSection.style.setProperty("position", "fixed", "important");
            searchSection.style.setProperty("top", "250px", "important");
            searchSection.style.setProperty("left", "360px", "important");
        }
    }
}

function hasEnoughSpacing() {
    const verticalListSection = document.querySelector('.home-vertical-list-section');
    const verticalBottom = verticalListSection.getBoundingClientRect().bottom;
    const spacing = window.innerHeight - verticalBottom;
    return spacing < 70;
}

// Divide categories and their sub-categories in columns evenly on footer
document.addEventListener("DOMContentLoaded", function () {
    const homeFooterItems = Array.from(document.querySelectorAll(".home-footer-item"));
    const totalItems = homeFooterItems.length;
    const columns = 4;
    const perColumn = Math.floor(totalItems / columns);
    const remainder = totalItems % columns;

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

$(document).on('click', '.home-stores-see-more', function (e) {
    e.preventDefault();
    loadMoreContent('stores');
});

$(document).on('click', '.home-collection-see-more', function (e) {
    e.preventDefault();
    loadMoreContent('collections');
});

$(document).on('click', '.home-promotion-see-more', function (e) {
    e.preventDefault();
    loadMoreContent('promotions');
});

function loadMoreContent(type) {
    let promotionsPageSize = parseInt(localStorage.getItem('promotionsPageSize')) || 0;
    let collectionsPageSize = parseInt(localStorage.getItem('collectionsPageSize')) || 0;
    let storesPageSize = parseInt(localStorage.getItem('storesPageSize')) || 0;
    let province = localStorage.getItem('selectedLocationCode');
    let cate = JSON.parse(localStorage.getItem('cate'));
    let districts = [];
    let districtButton = $('#district-dropdown-btn');

    if (districtButton.is('[district-code]')) {
        let districtCode = districtButton.attr('district-code');
        districts.push(districtCode);
    }

    switch (type) {
        case 'promotions':
            promotionsPageSize += 9;
            localStorage.setItem('promotionsPageSize', promotionsPageSize.toString());
            break;
        case 'collections':
            collectionsPageSize += 9;
            localStorage.setItem('collectionsPageSize', collectionsPageSize.toString());
            break;
        case 'stores':
            storesPageSize += 9;
            localStorage.setItem('storesPageSize', storesPageSize.toString());
            break;
    }

    $.ajax({
        url: `/Home/Index?province=${province}&categoryName=${cate}&districtsString=${districts}&collectionsPageSize=${collectionsPageSize}&storesListPageSize=${storesPageSize}&promotionsPageSize=${promotionsPageSize}`,
        type: 'POST',
        success: function (response) {
            let parsed = $('<div>').html(response);
            $('.home-stores-main').replaceWith(parsed.find('.home-stores-main'));
        },
        error: function () {
            toastr.error("Đã xảy ra lỗi!");
        }
    });
}
