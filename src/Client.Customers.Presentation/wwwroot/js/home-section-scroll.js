document.addEventListener("scroll", updateSearchSection);
document.querySelector(".home-search-section").addEventListener("mouseenter", updateSearchSection);

function updateSearchSection() {
    console.log("Updating styles...");

    const mainSection = document.querySelector(".home-main-section");
    const searchSection = document.querySelector(".home-search-section");

    if (hasEnoughSpacing()) {
        if (mainSection.getBoundingClientRect().bottom <= window.innerHeight) {
            console.log("Scrolled to bottom!");
            searchSection.style.setProperty("position", "relative", "important");
            searchSection.style.setProperty("top", "830px", "important");
            searchSection.style.setProperty("left", "10px", "important");
        } else {
            console.log("Scrolling...");
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
