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

// "Đồ ăn" category on nav-bar will be clicked on first load by default
window.onload = function () {
    if (!sessionStorage.getItem("foodClicked")) {
        document.querySelector('.nav-link[code-name="food"]').click();
        sessionStorage.setItem("foodClicked", "true");
    }
};
