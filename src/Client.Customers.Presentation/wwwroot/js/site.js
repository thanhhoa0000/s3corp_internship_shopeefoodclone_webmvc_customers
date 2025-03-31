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

function getItemsForStorePage(province, districts, category, subcategories) {
    $.ajax({
        url: `/Store/Promotions?province=${province}&categoryName=${category}&districtsString=${districts}&subcategoriesString=${subcategories}`,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var tempDom = $('<div></div>').html(response);
            var newStoresSection = tempDom.find('.store-main-content').html();            
            $('.store-main-content').html(newStoresSection);
            $('.store-section-options p').html(tempDom.find('.store-section-options p').html());

            if (subcategories.length > 0) {
                setTimeout(function() {
                    $('.store-category-item input').each(function() {
                        if (subcategories.includes(this.id)) {
                            $(this).prop('checked', true);
                            
                            let filterTag = document.querySelector('.store-section-category-filter-tag');
                            let checkboxes = document.querySelectorAll('.store-category-item input');
                            let checkedCount = Array.from(checkboxes).filter(checkbox => checkbox.checked).length;
                            
                            filterTag.style.display = 'flex';
                            filterTag.querySelector('span').textContent = `(${checkedCount})`;
                        }
                    });
                }, 100);  
            }
        }
    });
}

function checkUserIsAuthenticatedWithCallback(callback) {
    $.ajax({
        url: "/Account/IsUserAuthenticated",
        type: "GET",
        success: function(response) {
            if (!response.isAuthenticated){
                window.location.href = "/Account/Login";
            }
            else {
                if (typeof callback === "function") {
                    callback(true);
                }
            }
        },
        error: function() {
            console.error("Error checking authentication!");
            if (typeof callback === 'function') {
                callback(false);
            }
        }
    });
}
