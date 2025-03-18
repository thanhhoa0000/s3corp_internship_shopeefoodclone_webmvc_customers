document.addEventListener("scroll", updateSearchSection);
document.querySelector(".home-search-section").addEventListener("mouseenter", updateSearchSection);

function updateSearchSection() {

    const mainSection = document.querySelector(".home-main-section");
    const verticalListSection = document.querySelector('.home-vertical-list-section');
    const searchSection = document.querySelector(".home-search-section");

    if (hasEnoughSpacing()) {
        if (mainSection.getBoundingClientRect().bottom <= window.innerHeight) {
            var newTop = verticalListSection.offsetHeight - searchSection.offsetHeight - 205 + "px";
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
