document.addEventListener("configLoaded", getProvinces);
$(document).on("keypress", ".customer-phone-number input", function (event) {
    if (!/[0-9]/.test(event.key)) {
        event.preventDefault();
    }
});

$(document).on("click", ".customer-name i", function () {
    const $container = $(this).closest(".customer-name");
    const $span = $container.find("span");
    const $icon = $(this);
    
    if ($container.find("input").length) return;

    const currentName = $span.text();
    const $input = $(`<input class="edit-name-input ps-1 ms-1 me-1" type="text" value="${currentName}" />`);
    
    $input.on("click", function () {
        $(this).css('border-color', '#959595');
    });

    $input.on('blur', function () {
        const newText = $(this).val();

        if (!newText) {
            $(this).css('border-color', '#e74c3c');
            $(this).focus();
            return;
        }
        
        $(this).replaceWith(`<span class="ms-2">${newText}</span>`);
        $icon.show();
        $('#customer-name-input').val(newText); // optional hidden input update
    });

    $span.replaceWith($input);
    $input.focus();
    $icon.hide();
});

function proceedOrder() {
    const isProvinceSelected = isValidSelection("province-btn", "Tỉnh/Thành phố");
    const isDistrictSelected = isValidSelection("district-btn", "Quận/Huyện");
    const isWardSelected = isValidSelection("ward-btn", "Phường/Xã");
    const addressInput = document.querySelector('#detail-address').value.trim();
    const telephoneInput = document.querySelector('.customer-phone-number input').value.trim();

    if (isProvinceSelected && isDistrictSelected && isWardSelected && addressInput !== "" && telephoneInput !== "") {
        let streetAddress = addressInput;
        let ward = getSelectedText("ward-btn");
        let district = getSelectedText("district-btn");
        let province = getSelectedText("province-btn");
        let customerName = document.querySelector('.customer-basic-info .customer-name span').textContent.trim();
        
        ward = ward.split(/\s+/).length === 1 ? "Phường " + ward : ward;
        district = district.split(/\s+/).length === 1 ? "Quận " + district : district;
        
        const fullAddress = `${streetAddress}, ${ward}, ${district}, ${province}`;
        
        $("#full-address").val(fullAddress);
        $("#customer-name-input").val(customerName);
        
        return true;
    } else {
        $('.address-validation-message').show();
        if (!isProvinceSelected) {
            $('#province-btn').css('border', '1px solid #e74c3c');
        }
        if (!isDistrictSelected) {
            $('#district-btn').css('border', '1px solid #e74c3c');
        }
        if (!isWardSelected) {
            $('#ward-btn').css('border', '1px solid #e74c3c');
        }
        if (addressInput === ""){
            $('#detail-address').css('border', '1px solid #e74c3c');
        }
        if (telephoneInput === ""){
            let input = $('.customer-phone-number input')
            $.validator.unobtrusive.parse('form');
            input.valid();
            input.css('border', '1px solid #e74c3c');
        }
        return false;
    }
}

$(document).ready(function () {
    let cate = JSON.parse(localStorage.getItem('cate'));

    document.querySelectorAll(`.nav-link`).forEach((item) => {
        item.classList.remove("active");
    })

    document.querySelector(`.nav-link[code-name='${cate}']`).classList.add("active");
});

$(document).on("click", ".customer-address-section div div button", function () {
    const validationAddress = $('.address-validation-message');

    if (validationAddress.is(':visible')) {
        validationAddress.hide();
        $(this).css('border', '');
    }
});

$(document).on("click", "#detail-address", function () {
    const validationAddress = $('.address-validation-message');

    if (validationAddress.is(':visible')) {
        validationAddress.hide();
        $(this).css('border', '');
    }
});

$(document).on("click", '.customer-phone-number input', function () {
    const validationAddress = $('.address-validation-message');

    if (validationAddress.is(':visible')) {
        validationAddress.hide();
        $(this).css('border', '');
    }
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
            
            let districtsList = response.body.sort();

            districtsList.forEach(function (district) {
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
            
            let wardsList = response.body.sort();

            wardsList.forEach(function (ward) {
                let wardItem = `<li class="order-ward-item"><a class="dropdown-item" ward-code="${ward.code}">${ward.name.split(/\s+/).length === 1 ? "Phường" + ward.name : ward.name}</a></li>`;
                dropdown.append(wardItem);
            });

            dropdown.on('click', '.order-ward-item', function () {
                let button = $('#ward-btn');
                button.text(this.textContent);
            });
        }
    });
}

$(function () {
    $.validator.unobtrusive.parse('form');

    $('.customer-phone-number input').on('blur', function () {
        $(this).valid();
    });
});

function getSelectedText(buttonId) {
    const btn = document.getElementById(buttonId);
    return btn.textContent.trim();
}

function isValidSelection(buttonId, defaultText) {
    return getSelectedText(buttonId) !== defaultText;
}
