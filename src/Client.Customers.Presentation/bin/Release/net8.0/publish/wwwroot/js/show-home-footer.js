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
