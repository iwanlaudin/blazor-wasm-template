
export function addToggleEvent() {
    const body = document.querySelector('body');
    const toggleBtn = document.querySelector('.toggle-sidebar-btn');

    toggleBtn.addEventListener('click', function () {
        body.classList.toggle('toggle-sidebar');
    });
};

export function loadMap() {
    let map = L.map('map').setView([0, 0], 16);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 19 }).addTo(map);

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            let lat = position.coords.latitude;
            let lon = position.coords.longitude;
            map.setView([lat, lon], 9);
            L.marker([lat, lon]).addTo(map);
        });
    } else {
        console.log("Geolocation is not supported by this browser.");
    }
}