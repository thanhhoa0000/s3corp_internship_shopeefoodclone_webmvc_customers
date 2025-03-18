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

// Convert string to snake cake (E.g. Hồ Chí Minh -> ho_chi_minh)
function toSnakeCase(str) {
    return str
        .normalize("NFD")
        .replace(/[\u0300-\u036f]/g, '')
        .toLowerCase()
        .replace(/\s+/g, '_');
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

// "Đồ ăn" category on nav-bar will be clicked on first load by default
window.onload = function () {
    if (!sessionStorage.getItem("foodClicked")) {
        document.querySelector('.nav-link[code-name="food"]').click();
        sessionStorage.setItem("foodClicked", "true");
    }
};
