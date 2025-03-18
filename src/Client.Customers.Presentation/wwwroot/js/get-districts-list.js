function getDistricts(province) {
    $.ajax({
        url: `https://localhost:5001/districts?province=${province}`,
        type: 'GET',
        success: function (response) {
            if (!response || !response.isSuccessful || !Array.isArray(response.body)) {
                console.error("Invalid API response format:", response);
                return;
            }

            var dropdown = $('#district-dropdown');
            dropdown.empty();

            response.body.forEach(function (district) {
                var districtItem = `<li class="district-item"><a class="dropdown-item" onclick="getStoresByDistrict('${province}', toSnakeCase('${district.name}'))" data-district="${district.name}">${district.name.trim().split(/\s+/).length === 1 ? "Quận " + district.name : district.name}</a></li>`;
                dropdown.append(districtItem);
            });

            dropdown.on('click', '.dropdown-item', function () {
                var selectedDistrict = String($(this).data('district') || "").trim();
                $('#district-dropdown-btn').text(selectedDistrict.split(/\s+/).length === 1 ? "Quận " + selectedDistrict : selectedDistrict);
                localStorage.setItem("selectedDistrict", selectedDistrict);
            });
        },
        error: function () {
            console.error("Failed to fetch districts.");
        }
    });
}
