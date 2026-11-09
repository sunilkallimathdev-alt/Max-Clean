$(document).ready(function () {
    'use strict';

    //===== Map =====//
    function initialize() {
        var myLatlng = new google.maps.LatLng(41.8302481, -88.0137743,17);
        var mapOptions = {
            zoom: 10,
            disableDefaultUI: true,
            scrollwheel: false,
            center: myLatlng,
            styles: [
                {
                    featureType: 'all',
                    stylers: [
                        {saturation: -100}
                    ]
                }, {
                    featureType: 'water',
                    elementType: 'geometry',
                    stylers: [
                        {color: '#c5c5c5'}
                    ]
                }, {
                    featureType: 'poi.business',
                    elementType: 'labels',
                    stylers: [
                        {visibility: 'off'}
                    ]
                }
            ]
        }
        var map = new google.maps.Map(document.getElementById('g-mp'), mapOptions);

        var image = 'images/mp-icn.png';
        var myLatLng = new google.maps.LatLng(41.8302481, -88.0137743,17);
        var beachMarker = new google.maps.Marker({
            position: myLatLng,
            map: map,
            icon: image
        });

    }
    google.maps.event.addDomListener(window, 'load', initialize);
});