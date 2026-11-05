// Shared map setup, used by both the map page and the form's position picker.
const norwayBounds = [[57.9, 4.5], [71.2, 31.1]];

function createBaseMap(elementId) {
    const map = L.map(elementId);

    // No maxBounds: this is only where the map starts, and users can pan anywhere.
    map.fitBounds(norwayBounds);

    L.tileLayer("https://tile.openstreetmap.org/{z}/{x}/{y}.png", {
        maxZoom: 19,
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    return map;
}
