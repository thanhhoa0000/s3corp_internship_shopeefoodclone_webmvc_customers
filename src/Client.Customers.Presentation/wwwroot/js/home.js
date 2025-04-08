document.addEventListener("scroll", updateSearchSection);
document.querySelector(".home-search-section").addEventListener("mouseenter", updateSearchSection);
window.addEventListener("pageshow", getProvinces);

$(document).ready(function () {
    let cate = JSON.parse(localStorage.getItem('cate'));
    
    if (!cate) {
        localStorage.setItem('cate', JSON.stringify("food"));
    }

    document.querySelectorAll(`.main-nav-item`).forEach((item) => {
        item.classList.remove("active");
    })

    $(`.main-nav-item[code-name='${cate}']`).addClass("active");
});

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
