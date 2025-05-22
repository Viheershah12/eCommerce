let billingMap, billingMarker, billingInfoWindow;
let shippingMap, shippingMarker, shippingInfoWindow;

async function initMap() {
    const [{ Map }, { AdvancedMarkerElement }] = await Promise.all([
        google.maps.importLibrary("maps"),
        google.maps.importLibrary("marker"),
    ]);

    const defaultCenter = { lat: -1.286389, lng: 36.817223 }; // Example: Nairobi

    // Initialize Billing Map
    billingMap = new Map(document.getElementById("billing-map"), {
        center: defaultCenter,
        zoom: 13,
        mapId: "billing-map"
    });
    billingMarker = new AdvancedMarkerElement({ map: billingMap });
    billingInfoWindow = new google.maps.InfoWindow({});

    // Initialize Shipping Map
    shippingMap = new Map(document.getElementById("shipping-map"), {
        center: defaultCenter,
        zoom: 13,
        mapId: "shipping-map"
    });
    shippingMarker = new AdvancedMarkerElement({ map: shippingMap });
    shippingInfoWindow = new google.maps.InfoWindow({});

    // Add listeners to each autocomplete
    document.querySelectorAll("gmp-place-autocomplete").forEach((autocompleteEl) => {
        autocompleteEl.addEventListener("gmp-select", async (event) => {
            const prefix = autocompleteEl.dataset.prefix;
            const place = event.placePrediction.toPlace();

            await place.fetchFields({
                fields: ["displayName", "formattedAddress", "addressComponents", "viewport"]
            });

            updateMapAndFields(place, prefix);
        });
    });

    billingMap.addListener("click", (event) => {
        handleMapClick(event, "Billing");
    });

    shippingMap.addListener("click", (event) => {
        handleMapClick(event, "Shipping");
    });
}

function handleMapClick(event, prefix) {
    const geocoder = new google.maps.Geocoder();
    geocoder.geocode({ location: event.latLng }, (results, status) => {
        if (status === "OK" && results[0]) {
            const place = {
                displayName: results[0].formatted_address,
                formattedAddress: results[0].formatted_address,
                addressComponents: results[0].address_components,
                location: event.latLng
            };
            updateMapAndFields(place, prefix);
        }
    });
}

function updateMapAndFields(place, prefix) {
    const map = prefix === "Billing" ? billingMap : shippingMap;
    const marker = prefix === "Billing" ? billingMarker : shippingMarker;
    const infoWindow = prefix === "Billing" ? billingInfoWindow : shippingInfoWindow;

    const location = place.location;
    marker.position = location;

    const content = `
        <div><strong>${place.displayName}</strong><br />${place.formattedAddress}</div>
      `;

    infoWindow.setContent(content);
    infoWindow.setPosition(location);
    infoWindow.open(map, marker);

    if (place.viewport) {
        map.fitBounds(place.viewport);
    } else {
        map.setCenter(location);
        map.setZoom(17);
    }

    fillFields(place, prefix);
}

function fillFields(place, prefix) {
    const components = place.addressComponents;
    const get = (type) => components?.find(c => c.types.includes(type))?.longText || "";

    document.getElementById(`${prefix}_Country`).value = get("country");
    document.getElementById(`${prefix}_Locality`).value = get("locality");
    document.getElementById(`${prefix}_PostalCode`).value = get("postal_code");
    document.getElementById(`${prefix}_AdministrativeAreaLevel1`).value = get("administrative_area_level_1");
    document.getElementById(`${prefix}_AdministrativeAreaLevel2`).value = get("administrative_area_level_2");
    document.getElementById(`${prefix}_Sublocality`).value = get("sublocality");
    document.getElementById(`${prefix}_StreetNumber`).value = get("street_number");
    document.getElementById(`${prefix}_UnitNumber`).value = ""; // Optional
    document.getElementById(`${prefix}_BuildingName`).value = place.displayName;
    document.getElementById(`${prefix}_Latitude`).value = location.lat;
    document.getElementById(`${prefix}_Longitude`).value = location.lng;

    document.getElementById('addressComponents').classList.remove('d-none');
}
