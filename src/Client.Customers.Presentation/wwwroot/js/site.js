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
                dropdown.append(`<li class="location-item"><a class="dropdown-item" onclick="getStores('${toSnakeCase(location.name)}')" data-location="${location.name}">${location.name}</a></li>`);
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
                        console.log("clicked!");
                    }
                });
            }, 100);
        },
        error: function () {
            console.error("Failed to fetch locations.");
        }
    });
});

function getStores(province) {
    $.ajax({
        type: 'POST',
        url: `/Home/Index?province=${encodeURIComponent(province)}`,
        success: function (response) {
            var tempDom = $('<div></div>').html(response);
            var newContent = tempDom.find('.home-main-section').html();
            $('.home-main-section').html(newContent);
            console.log(province);
            console.log(newContent);
        },
        error: function () {
            console.error("Failed to fetch stores.");
        }
    });
}

document.addEventListener("DOMContentLoaded", function () {
    const navLinks = document.querySelectorAll(".main-nav .main-nav-item");

    navLinks.forEach(link => {
        link.addEventListener("click", function () {
            navLinks.forEach(item => item.classList.remove("active"));
            this.classList.add("active");
        });
    });
});

function toSnakeCase(str) {
    return str
        .normalize("NFD")
        .replace(/[\u0300-\u036f]/g, '')
        .toLowerCase()
        .replace(/\s+/g, '_');
}

document.addEventListener("DOMContentLoaded", function () {
    let angle = 0;
    document.querySelector(".rotate-icon").addEventListener("click", function () {
        angle += 360;  // Increase angle on every click
        this.querySelector("i").style.transform = `rotate(${angle}deg)`;
    });
});
