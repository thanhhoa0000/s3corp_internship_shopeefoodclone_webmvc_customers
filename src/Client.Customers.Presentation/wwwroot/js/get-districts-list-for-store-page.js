$(document).ready(function () {
    console.log("🚀 Script loaded");
    getDistrictListForStorePage();
});

function getDistrictListForStorePage() {
    console.log("🚀 Script loaded");
    var province = document.querySelector('#location-dropdown-btn').getAttribute('province-code');
    
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
                        <div class="form-check store-district-item">
                            <input class="form-check-input" type="checkbox" id="${district.code}" value="${district.name}" />
                            <label class="form-check-label" for="${district.code}">${district.name.trim().split(/\s+/).length === 1 ? "Quận " + district.name : district.name}</label>
                        </div>
                    </div>`
                dropdown.append(districtItem);
                console.log(districtItem)
            });
        },
        error: function () {
            console.error("Failed to fetch districts.");
        }
    });
}
