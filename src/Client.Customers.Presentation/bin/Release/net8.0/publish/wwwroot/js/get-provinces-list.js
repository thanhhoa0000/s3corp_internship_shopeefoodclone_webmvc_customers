$(document).ready(function () {
    $.ajax({
        url: `${gatewayUrl}/provinces/with-stores-count`,
        type: 'GET',
        success: function (response) {
            if (!response.isSuccessful || !response.body) {
                console.error("Invalid API response");
                return;
            }

            var dropdown = $('#location-dropdown');
            dropdown.empty();

            var locations = response.body;

            var selectedLocation = localStorage.getItem("selectedLocation") || "Hồ Chí Minh";
            $('#location-dropdown-btn').text(selectedLocation);

            locations.sort((a, b) => {
                if (a.codeName === "ho_chi_minh") return -1;
                if (b.codeName === "ho_chi_minh") return 1;
                if (a.codeName === "ha_noi") return -1;
                if (b.codeName === "ha_noi") return 1;
                return 0;
            });

            locations.forEach(function (location) {
                var listItem = $(`
                            <li class="location-item">
                                <a class="dropdown-item d-flex justify-content-between" onclick="getStores('${location.code}', '${location.name}')" data-location="${location.name}" province-code="${location.code}">${location.name} <span class="col">${location.storesCount} địa điểm</span></a>
                            </li>
                        `);
                dropdown.append(listItem);
            });

            $('.dropdown-item').click(function () {
                var selectedLocation = $(this).data('location');
                var provinceCode = $(this).attr('province-code');
                var dropdownBtn = $('#location-dropdown-btn');

                dropdownBtn.text(selectedLocation);
                dropdownBtn.attr('province-code', provinceCode);

                localStorage.setItem("selectedLocation", selectedLocation);
            });

            setTimeout(function () {
                $('.dropdown-item').each(function () {
                    if ($(this).data('location') === selectedLocation) {
                        $(this).trigger('click');
                    }
                });
            }, 100);
        },
        error: function () {
            console.error("Failed to fetch locations.");
        }
    });
});
