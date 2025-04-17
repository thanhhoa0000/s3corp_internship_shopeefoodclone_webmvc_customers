document.addEventListener("scroll", updateSearchSection);
document.querySelector(".home-search-section").addEventListener("mouseenter", updateSearchSection);
window.addEventListener("pageshow", getProvinces);

$(document).ready(function () {
    let cate = JSON.parse(localStorage.getItem('cate'));

    if (!cate) {
        cate = "food";
        localStorage.setItem('cate', JSON.stringify(cate));
    }

    document.querySelectorAll(`.main-nav-item`).forEach((item) => {
        item.classList.remove("active");
    })

    $(`.main-nav-item[code-name='${cate}']`).addClass("active");

    localStorage.setItem('promotionsPageNumber', "1");
    localStorage.setItem('collectionsPageNumber', "1");
    localStorage.setItem('storesPageNumber', "1");
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
    let promotionsPageNumber = parseInt(localStorage.getItem('promotionsPageNumber')) || 1;
    let storesPageNumber = parseInt(localStorage.getItem('storesPageNumber')) || 1;
    let collectionsPageNumber = parseInt(localStorage.getItem('collectionsPageNumber')) || 1;
    let province = localStorage.getItem('selectedLocationCode');
    let cate = JSON.parse(localStorage.getItem('cate'));
    let districts = [];
    let districtButton = $('#district-dropdown-btn');
    let request;
    let pageNumber;
    let seeMoreButton;
    let list;
    let itemSelector;

    if (districtButton.is('[district-code]')) {
        let districtCode = districtButton.attr('district-code');
        districts.push(districtCode);
    }

    switch (type) {
        case 'promotions':
            request = "UpdatePromotionsList";
            promotionsPageNumber += 1;
            localStorage.setItem('promotionsPageNumber', promotionsPageNumber.toString());
            pageNumber = promotionsPageNumber;
            seeMoreButton = $('.home-promotion-see-more');
            list = $('.home-promotion-lists');
            itemSelector = '.store-item';
            break;
        case 'collections':
            request = "UpdateCollectionsList";
            collectionsPageNumber += 1;
            localStorage.setItem('collectionsPageNumber', collectionsPageNumber.toString());
            pageNumber = collectionsPageNumber;
            seeMoreButton = $('.home-collection-see-more');
            list = $('.home-collection-lists');
            itemSelector = '.collection-item';
            break;
        case 'stores':
            request = "UpdateStoresList";
            storesPageNumber += 1;
            localStorage.setItem('storesPageNumber', storesPageNumber.toString());
            pageNumber = storesPageNumber;
            seeMoreButton = $('.home-stores-see-more');
            list = $('.home-stores-list');
            itemSelector = '.home-store-item';
            break;
    }

    $.ajax({
        url: `/Home/${request}?province=${province}&categoryName=${cate}&districtsString=${districts}&pageNumber=${pageNumber}`,
        type: 'POST',
        success: function (response) {
            let parsed = $('<div>').html(response);
            let items = parsed.find(itemSelector);
            let itemsQuantity = items.length;
            list.append(items);

            if (itemsQuantity < 9) {
                seeMoreButton.hide();
            }
        },
        error: function () {
            toastr.error("Đã xảy ra lỗi!");
        }
    });
}
