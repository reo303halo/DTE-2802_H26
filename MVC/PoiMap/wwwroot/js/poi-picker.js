(function () {
    const latitudeInput = document.getElementById("Latitude");
    const longitudeInput = document.getElementById("Longitude");
    const map = createBaseMap("picker-map");
    let marker = null;

    function placeMarker(latitude, longitude) {
        if (marker) {
            marker.setLatLng([latitude, longitude]);
        } else {
            marker = L.marker([latitude, longitude]).addTo(map);
        }
    }

    function readInputs() {
        const latitude = Number.parseFloat(latitudeInput.value);
        const longitude = Number.parseFloat(longitudeInput.value);

        if (Number.isNaN(latitude) || Number.isNaN(longitude)) {
            return null;
        }

        return { latitude, longitude };
    }

    map.on("click", event => {
        // wrap() keeps the longitude within -180..180 if the map has been panned around the world.
        const { lat, lng } = event.latlng.wrap();
        latitudeInput.value = lat.toFixed(6);
        longitudeInput.value = lng.toFixed(6);
        placeMarker(lat, lng);
    });

    function updateMarkerFromInputs() {
        const position = readInputs();
        if (position) {
            placeMarker(position.latitude, position.longitude);
        }
    }

    latitudeInput.addEventListener("change", updateMarkerFromInputs);
    longitudeInput.addEventListener("change", updateMarkerFromInputs);

    // When editing, or after a failed validation: show the existing position.
    const position = readInputs();
    if (position) {
        placeMarker(position.latitude, position.longitude);
        map.setView([position.latitude, position.longitude], 9);
    }
})();
