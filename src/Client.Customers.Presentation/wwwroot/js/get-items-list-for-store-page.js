$(document).ready(function () {
    let provinceTargetNode = document.getElementById("location-dropdown-btn");
    let districtDropdown = document.getElementById("store-district-dropdown");
    let categoryDropdown = document.getElementById("store-category-dropdown");
    let clickCount = 0;
    
    let category = JSON.parse(localStorage.getItem('cate'));
    let subCategory = JSON.parse(localStorage.getItem('sub-category'));

    let provinceButtonObserver = new MutationObserver(() => {
        let province = $("#location-dropdown-btn").attr("province-code");
        
        if (province) {
            getDistrictListForStorePage(province);
            getSubCategoriesListForStorePage(category);
            if (subCategory) {
                getItemsForStorePage(province, [], category, [subCategory]);
                localStorage.setItem('sub-category', null);
            }
            else {
                getItemsForStorePage(province, [], category, []);
            }
            provinceButtonObserver.disconnect();
        }
    });
    
    let districtDropdownButtonObserver = new MutationObserver((mutations) => {
        mutations.forEach((mutation) => {
            if (mutation.type === 'attributes' && mutation.attributeName === 'class' && !mutation.target.classList.contains('show')) {
                let districtCodes = []
                let subcategoryCodes = []
                let province = $("#location-dropdown-btn").attr("province-code");
                
                document.querySelectorAll('.store-district-item .form-check-input:checked').forEach(checkbox => {
                    districtCodes.push(checkbox.id);
                });

                document.querySelectorAll('.store-category-item .form-check-input:checked').forEach(checkbox => {
                    subcategoryCodes.push(checkbox.id);
                });

                getItemsForStorePage(province, districtCodes, category, subcategoryCodes);
            }
        });
    });

    let categoryDropdownButtonObserver = new MutationObserver((mutations) => {
        mutations.forEach((mutation) => {
            if (mutation.type === 'attributes' && mutation.attributeName === 'class' && !mutation.target.classList.contains('show')) {
                let districtCodes = []
                let subcategoryCodes = []
                let province = $("#location-dropdown-btn").attr("province-code");
                
                document.querySelectorAll('.store-district-item .form-check-input:checked').forEach(checkbox => {
                    districtCodes.push(checkbox.id);
                });

                document.querySelectorAll('.store-category-item .form-check-input:checked').forEach(checkbox => {
                    subcategoryCodes.push(checkbox.id);
                });

                getItemsForStorePage(province, districtCodes, category, subcategoryCodes);
            }
        });
    });
        
    document.querySelector('.main-nav-item.active')?.classList.remove('active');
    document.querySelector(`.main-nav-item[code-name='${category}']`).classList.add('active');

    provinceButtonObserver.observe(provinceTargetNode, { attributes: true });

    districtDropdownButtonObserver.observe(districtDropdown, {
        attributes: true,
        attributeFilter: ['class']
    });

    categoryDropdownButtonObserver.observe(categoryDropdown, {
        attributes: true,
        attributeFilter: ['class']
    });
    
    $(document).on('click', '.location-item', function () {
        clickCount++;
        if (clickCount > 1) {
            console.log(clickCount);
            window.location.href = "/Home/Index";
        }
    });
    
    $(document).on('click', '.store-district-item .form-check-input', 
        () => initializeFilterTag(document.querySelectorAll(
            '.store-district-item .form-check-input'), 
            document.querySelector('.store-section-location-filter-tag')));

    $(document).on('click', '.store-category-item .form-check-input',
        () => initializeFilterTag(document.querySelectorAll(
                '.store-category-item .form-check-input'),
            document.querySelector('.store-section-category-filter-tag')));
    
    $(document).on('click', '.store-section-category-filter-tag button', function () {
        let districtCodes = []
        let subcategoryCodes = []
        let province = $("#location-dropdown-btn").attr("province-code");

        document.querySelectorAll('.store-category-item .form-check-input').forEach(checkbox => {
            checkbox.checked = false;
        });
        
        document.querySelector('.store-section-category-filter-tag').style.display = 'none';
        
        document.querySelectorAll('.store-district-item .form-check-input:checked').forEach(checkbox => {
            districtCodes.push(checkbox.id);
        });

        document.querySelectorAll('.store-category-item .form-check-input:checked').forEach(checkbox => {
            subcategoryCodes.push(checkbox.id);
        });

        getItemsForStorePage(province, districtCodes, category, subcategoryCodes);
    });

    $(document).on('click', '.store-section-location-filter-tag button', function () {
        let districtCodes = []
        let subcategoryCodes = []
        let province = $("#location-dropdown-btn").attr("province-code");
        
        document.querySelectorAll('.store-district-item .form-check-input').forEach(checkbox => {
            checkbox.checked = false;
        });

        document.querySelector('.store-section-location-filter-tag').style.display = 'none';        

        document.querySelectorAll('.store-district-item .form-check-input:checked').forEach(checkbox => {
            districtCodes.push(checkbox.id);
        });

        document.querySelectorAll('.store-category-item .form-check-input:checked').forEach(checkbox => {
            subcategoryCodes.push(checkbox.id);
        });

        getItemsForStorePage(province, districtCodes, category, subcategoryCodes);
    });
});

function getDistrictListForStorePage(province) {    
    $.ajax({
        url: `https://localhost:5001/districts?province=${province}`,
        type: 'GET',
        success: function (response) {
            if (!response || !response.isSuccessful || !Array.isArray(response.body)) {
                console.error("Invalid API response format:", response);
                return;
            }

            var dropdown = $('#store-district-dropdown .row');
            dropdown.empty();

            response.body.forEach(function (district) {
                var districtItem = 
                    `<div class="col-4">
                        <div class="form-check store-district-item ps-5">
                            <input class="form-check-input" type="checkbox" id="${district.code}" value="${district.name}" />
                            <label class="form-check-label" for="${district.code}">${district.name.trim().split(/\s+/).length === 1 ? "Quận " + district.name : district.name}</label>
                        </div>
                    </div>`
                dropdown.append(districtItem);
            });
        },
        error: function () {
            console.error("Failed to fetch districts.");
        }
    });
}
