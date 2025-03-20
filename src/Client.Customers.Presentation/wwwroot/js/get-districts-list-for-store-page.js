$(document).ready(function () {
    let targetNode = document.getElementById("location-dropdown-btn");
    let clickCount = 0;

    let observer = new MutationObserver(() => {
        let province = $("#location-dropdown-btn").attr("province-code");

        if (province) {
            getDistrictListForStorePage(province);
            observer.disconnect();
        }
    });

    observer.observe(targetNode, { attributes: true });
    
    $(document).on('click', '.location-item', function () {
        clickCount++;
        if (clickCount > 1) {
            window.location.href = "/Home/Index";
        }
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
