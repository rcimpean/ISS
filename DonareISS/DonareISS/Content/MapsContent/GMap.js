//AIzaSyAEjIPgwtANC_dxfnSmOe2z2gOK6g6sGf0 google api key
var map;////callback=initMap
function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 46.77007, lng: 23.590352 },
        zoom: 14
    });
    var adrese = jQuery.getJSON("/Json/GetOperators", function (result) {
        return (result)
    });

  
    //console.log(result[0]);
    //conectare2 markere
    var Plan = [{ lat: 46.761887, lng: 23.565154 }
        , { lat: 46.7674953, lng: 23.591463900000008 }];
    var flightPath = new google.maps.Polyline({
        path: Plan,
        geodesic: true,
        strokeColor: '#FF0000',
        strokeOpacity: 1.0,
        strokeWeight: 2,
        map: map
    });
    //distante between raluHome and ubbCluj
    //----------------------------------------console.log(google.maps.geometry.spherical.computeDistanceBetween(LatLong, UBBLatLong));
    //convert adress
    var geocoder = new google.maps.Geocoder();
   /// geocodeAddress(geocoder, map,"Big Belly manastur");
}
function addMarker(map, position,title) {
    var marker = new google.maps.Marker({
        position: UBBLatLong,
        title: title,
        map: map
    });
}
function geocodeAddress(geocoder, resultsMap,address) {

    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === 'OK') {
            resultsMap.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: resultsMap,
                position: results[0].geometry.location
            });
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}



