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
        window.scrollTo({ top: 0, behavior: "smooth" });
    });
});