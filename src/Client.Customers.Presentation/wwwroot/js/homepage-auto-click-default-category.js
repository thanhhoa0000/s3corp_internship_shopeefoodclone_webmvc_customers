window.onload = function () {
    localStorage.setItem('cate', JSON.stringify("food"));
    let homeMainSection = document.querySelector('.home-main-section');

    if (!sessionStorage.getItem("foodClicked") && homeMainSection != null) {
        document.querySelector('.nav-link[code-name="food"]').click();
        sessionStorage.setItem("foodClicked", "true");
    }
};
