
export async function initMap(dataPoints) {
   
    const position = { lat: -8.620743, lng: 120.728321 }; 

    const { Map, InfoWindow } = await google.maps.importLibrary("maps");
    const { AdvancedMarkerElement, PinElement } = await google.maps.importLibrary("marker");

    let map = new Map(document.getElementById("map"), {
        zoom: 9,
        center: position,
        mapId: "DEMO_MAP_ID",
        mapTypeId: "hybrid",
        mapTypeControl: false,
        streetViewControl: false,
        gestureHandling: "greedy",
    });

    const infoWindow = new InfoWindow();

    function addMarker(data, map) {

        const icon = document.createElement("div");
        icon.innerHTML = '<i class="bi bi-droplet-half"></i>';

        const pin = new PinElement({
            glyph: icon,
            glyphColor: "white",
            background: "#0d6efd",
            borderColor: "black",
        });

        const marker = new AdvancedMarkerElement({
            map: map,
            position: { lat: data.latitude, lng: data.longitude },
            content: pin.element,
            title: data.title,
        });

        marker.data = {
            id: data.waterSupplyId,
        };     

        marker.addListener("click", () => {
            showDetailData(marker.data.id)
                .then(data => {
                    const htmlContent = setInfoWindowContent(data);
                    infoWindow.close();
                    infoWindow.setContent(htmlContent);
                    infoWindow.open(marker.map, marker);
                })
        });
    }

    dataPoints.forEach((dataPoint) => {
        addMarker(dataPoint, map)
    });
}

function showDetailData(id){
    return DotNet.invokeMethodAsync('BlazorWasmTemplate.Client', 'showDetailDataAsync', id);
};

function setInfoWindowContent(data) {
    const htmlContent = `
        <div id="content" style="width: 20rem;">
            <div id="bodyContent" class="card-body">
                <h5 class="fw-bold">${data.name}</h5>
                <div class="row">
                    <div class="col-4 fw-bold ">Type</div>
                    <div class="col-8">${data.type}</div>
                </div>
                <div class="row">
                    <div class="col-4 fw-bold">Ownership</div>
                    <div class="col-8">${data.ownershipNorm}</div>
                </div>
                <div class="row">
                    <div class="col-4 fw-bold">Area Size</div>
                    <div class="col-8">${data.areaSize}</div>
                </div>
                <div class="row">
                    <div class="col-4 fw-bold">Population</div>
                    <div class="col-8">${data.totalPopulation}</div>
                </div>
                    <div class="row">
                    <div class="col-4 fw-bold">Highest Elevation</div>
                    <div class="col-8">${data.highestElevation}</div>
                </div>
                    <div class="row">
                    <div class="col-4 fw-bold">Lowest Elevation</div>
                    <div class="col-8">${data.lowestElevation}</div>
                </div>
                    <div class="row">
                    <div class="col-4 fw-bold">Avg Elevation</div>
                    <div class="col-8">${data.avgElevation}</div>
                </div>
                    <div class="row">
                    <div class="col-4 fw-bold">Rainfall</div>
                    <div class="col-8">${data.rainfall}</div>
                </div>
                <div class="row">
                    <div class="col-4 fw-bold">Start Survey</div>
                    <div class="col-8">${data.startSurveyDt}</div>
                </div>
                <div class="row">
                    <div class="col-4 fw-bold">End Survey</div>
                    <div class="col-8">${data.endSurveyDt}</div>
                </div>
            </div>
        </div>
        `;

    return htmlContent;
}