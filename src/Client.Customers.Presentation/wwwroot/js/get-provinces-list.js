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

            locations.forEach(function (location) {
                var province = toSnakeCase(location.name);

                var listItem = $(`
                    <li class="location-item row">
                        <a class="dropdown-item col" onclick="getStores('${province}')" data-location="${location.name}">${location.name}</a>
                        <span class="col">Loading...</span> 
                    </li>
                `);

                dropdown.append(listItem);
                
                getStoresCountByProvince(province).done(function (count) {
                    listItem.find('span').text(`${count} địa điểm`);
                }).fail(function () {
                    listItem.find('span').text(`0 địa điểm`);
                });
            });

            $('.dropdown-item').click(function () {
                var selectedLocation = $(this).data('location');
                $('#location-dropdown-btn').text(selectedLocation);

                localStorage.setItem("selectedLocation", selectedLocation);
            });

            setTimeout(function () {
                $('.dropdown-item').each(function () {
                    if ($(this).data('location') === selectedLocation) {
                        $(this).trigger('click');
                        getDistricts(toSnakeCase($(this).data('location')));
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
        url: `https://localhost:5001/stores/${province}`,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            locationRequest: { province: province }
        }),
        dataType: 'json'
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
