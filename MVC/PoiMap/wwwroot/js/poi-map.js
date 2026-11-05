(function () {
    const mapElement = document.getElementById("map");
    const { markersUrl, editUrl, deleteUrl } = mapElement.dataset;
    const map = createBaseMap("map");

    // Built with DOM methods and textContent rather than an HTML string,
    // so text entered by users can never run as HTML or script.
    function addElement(parent, tag, className, text) {
        const element = document.createElement(tag);
        element.className = className;
        element.textContent = text;
        parent.appendChild(element);
        return element;
    }

    function createPopup(poi) {
        const popup = document.createElement("div");

        addElement(popup, "div", "poi-popup-title", poi.name);
        addElement(popup, "div", "poi-popup-category", poi.category);

        if (poi.description) {
            addElement(popup, "p", "", poi.description).style.margin = "0.5rem 0";
        }

        addElement(popup, "div", "small", `${poi.latitude.toFixed(4)}, ${poi.longitude.toFixed(4)}`);

        const actions = addElement(popup, "div", "poi-popup-actions mt-2", "");
        addElement(actions, "a", "", "Edit").href = `${editUrl}/${poi.id}`;
        addElement(actions, "a", "text-danger", "Delete").href = `${deleteUrl}/${poi.id}`;

        return popup;
    }

    async function loadMarkers() {
        const response = await fetch(markersUrl);
        if (!response.ok) {
            throw new Error(`Request failed with status ${response.status}`);
        }

        const pois = await response.json();

        for (const poi of pois) {
            L.marker([poi.latitude, poi.longitude], { title: poi.name })
                .addTo(map)
                .bindPopup(createPopup(poi));
        }
    }

    loadMarkers().catch(error => {
        console.error(error);
        const errorElement = document.getElementById("map-error");
        errorElement.textContent = "Could not load the points of interest.";
        errorElement.classList.remove("d-none");
    });
})();
