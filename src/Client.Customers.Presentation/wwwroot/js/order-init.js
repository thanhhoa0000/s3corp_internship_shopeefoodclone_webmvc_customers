document.addEventListener("configLoaded", getProvinces);
document.querySelector(".customer-phone-number input").addEventListener("keypress", function (event) {
    if (!/[0-9]/.test(event.key)) {
        event.preventDefault();
    }
})

$(document).ready(function () {
    let cate = JSON.parse(localStorage.getItem('cate'));

    document.querySelectorAll(`.nav-link`).forEach((item) => {
        item.classList.remove("active");
    })

    document.querySelector(`.nav-link[code-name='${cate}']`).classList.add("active");
});

function getProvinces() {
    $.ajax({
        url: `${gatewayUrl}/provinces`,
        type: 'GET',
        success: function (response) {
            if (!response || !response.isSuccessful || !Array.isArray(response.body)) {
                console.error("Invalid API response format:", response);
                return;
            }

            let dropdown = $('#order-province-dropdown');
            dropdown.empty();

            response.body.sort((a, b) => {
                if (a.codeName === "ho_chi_minh") return -1;
                if (b.codeName === "ho_chi_minh") return 1;
                if (a.codeName === "ha_noi") return -1;
                if (b.codeName === "ha_noi") return 1;
                return 0;
            });

            response.body.forEach(function (province) {
                let provinceItem = `<li class="order-province-item"><a class="dropdown-item" province-code="${province.code}" onclick="getDistricts('${province.code}')">${province.name}</a></li>`
                dropdown.append(provinceItem);
            });

            dropdown.on('click', '.order-province-item', function () {
                let button = $('#province-btn');
                if (this.textContent !== button.text()) {
                    $('#district-btn').text("Quận/Huyện")
                }
                button.text(this.textContent);
            });
        },
        error: function () {
            console.error("Failed to fetch provinces.");
        }
    });
}

function getDistricts(province) {
    $.ajax({
        url: `${gatewayUrl}/districts?province=${province}`,
        type: 'GET',
        success: function (response) {
            if (!response || !response.isSuccessful || !Array.isArray(response.body)) {
                console.error("Invalid API response format:", response);
                return;
            }

            let dropdown = $('#order-district-dropdown');
            dropdown.empty();

            response.body.forEach(function (district) {
                let districtItem = `<li class="order-district-item"><a class="dropdown-item" onclick="getWards('${district.code}');" district-code="${district.code}">${district.name.trim().split(/\s+/).length === 1 ? "Quận " + district.name : district.name}</a></li>`;
                dropdown.append(districtItem);
            });

            dropdown.on('click', '.order-district-item', function () {
                let button = $('#district-btn');
                if (this.textContent !== button.text()) {
                    $('#ward-btn').text("Phường/Xã")
                }
                button.text(this.textContent);
            });
        },
        error: function () {
            console.error("Failed to fetch districts.");
        }
    });
}

function getWards(district) {
    $.ajax({
        url: `${gatewayUrl}/wards?district=${district}`,
        type: 'GET',
        success: function (response) {
            if (!response || !response.isSuccessful || !Array.isArray(response.body)) {
                console.error("Invalid API response format:", response);
                return;
            }
            
            let dropdown = $('#order-ward-dropdown');
            dropdown.empty();

            response.body.forEach(function (ward) {
                let wardItem = `<li class="order-ward-item"><a class="dropdown-item" ward-code="${ward.code}">${ward.name}</a></li>`;
                dropdown.append(wardItem);
            });

            dropdown.on('click', '.order-ward-item', function () {
                let button = $('#ward-btn');
                button.text(this.textContent);
            });
        }
    });
}
