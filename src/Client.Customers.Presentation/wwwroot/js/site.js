﻿$(document).ready(function () {
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
                dropdown.append(`<li class="location-item"><a class="dropdown-item" href="#" data-location="${location.name}">${location.name}</a></li>`);
            });
            
            $('.dropdown-item').click(function () {
                var selectedLocation = $(this).data('location');
                $('#location-dropdown-btn').text(selectedLocation);
                
                localStorage.setItem("selectedLocation", selectedLocation);
            });
        },
        error: function () {
            console.error("Failed to fetch locations.");
        }
    });
});


