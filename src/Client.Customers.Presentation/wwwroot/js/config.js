let gatewayUrl = {};

fetch('/Configuration/Get')
    .then(response => response.json())
    .then(config => {
        gatewayUrl = config.gatewayUrl;
    })
    .catch(error => console.log(error));
