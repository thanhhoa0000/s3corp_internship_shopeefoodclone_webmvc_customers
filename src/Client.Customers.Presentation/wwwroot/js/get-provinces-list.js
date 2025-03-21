$(document).ready(function () {
    $.ajax({
        url: 'https://localhost:5001/provinces',
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
                        <a class="dropdown-item d-flex justify-content-between" onclick="getStores('${location.code}', '${location.name}')" data-location="${location.name}" province-code="${location.code}">${location.name} <span class="col">Loading...</span></a>
                    </li>
                `);

                dropdown.append(listItem);
                
                getStoresCountByProvince(location.code).done(function (count) {
                    listItem.find('a span').text(`${count} địa điểm`);
                }).fail(function () {
                    listItem.find('a span').text(`0 địa điểm`);
                });
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

function getStoresCountByProvince(province) {
    return $.ajax({
        url: `https://localhost:5001/stores/get`,
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({
            locationRequest: { province: province }
        })
    }).then(function (response) {
        if (!response.isSuccessful || !response.body) {
            console.error("Invalid API response");
            return 0;
        }
        return response.body.length;
    }).catch(function (error) {
        console.error("Request failed:", error);
        return 0;
    });
}
