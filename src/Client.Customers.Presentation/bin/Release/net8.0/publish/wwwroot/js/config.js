let gatewayUrl = {};

fetch('/Configuration/Get')
    .then(response => response.json())
    .then(config => {
        gatewayUrl = config.gatewayUrl;
        document.dispatchEvent(new Event("configLoaded"));
    })
    .catch(error => console.log(error));
