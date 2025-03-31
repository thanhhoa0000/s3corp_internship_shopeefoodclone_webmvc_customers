$(document).ready(function () {
    let cate = JSON.parse(localStorage.getItem('cate'));
    const menuItems = document.querySelectorAll(".menu .item");
    
    document.querySelectorAll(`.nav-link`).forEach((item) => {
        item.classList.remove("active");
        item.removeAttribute("onclick");
    })
    
    document.querySelector(`.nav-link[code-name='${cate}']`).classList.add("active");

    menuItems.forEach(link => {
        link.addEventListener("click", function () {
            menuItems.forEach(item => item.classList.remove("active"));
            this.classList.add("active");
        });
    });
});

function handleButtonClick(button, customerId) {
    checkUserIsAuthenticatedWithCallback(function(isAuthenticated) {
        if (isAuthenticated) {
            if (customerId) {
                window.location.href = `/Cart/Index?customerId=${customerId}`;
            }
            else {
                console.error("CustomerId not found!");
            }
        }
    });
    
    return false;
}
