function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: -33.8688, lng: 151.2195 },
        zoom: 13
    });
    var origin1 = { lat: 53.8455148, lng: 27.466352099999995 };
    //var origin2 = "Greenwich, England";
    //var destinationA = "Stockholm, Sweden";
    var destinationB = { lat: 53.9032045, lng: 27.536133699999937 };


    var service = new google.maps.DistanceMatrixService();
    service.getDistanceMatrix(
      {
          origins: [origin1],
          destinations: [destinationB],
          travelMode: google.maps.TravelMode.DRIVING,
          //transitOptions: TransitOptions,
          //drivingOptions: DrivingOptions,
          //unitSystem: UnitSystem,
          avoidHighways: false,
          avoidTolls: false,
      }, callback);

    function callback(response, status) {
        // See Parsing the Results for
        // the basics of a callback function.
        var orig = document.getElementById("orig"),
        dest = document.getElementById("dest"),
        dist = document.getElementById("dist");

        if (status == "OK") {
            orig.value = response.originAddresses[0];
            dest.value = response.destinationAddresses[0];
            dist.value = response.rows[0].elements[0].distance.text;
            console.log(response.originAddresses[0]);
            console.log(response.destinationAddresses[0]);
            console.log(response.rows[0].elements[0].distance.text);
        } else {
            alert("Error: " + status);
        }
    }
    var input = /** @type {!HTMLInputElement} */(
        document.getElementById('pac-input'));

    var types = document.getElementById('type-selector');
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(types);

    var autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo('bounds', map);

    var infowindow = new google.maps.InfoWindow();
    var marker = new google.maps.Marker({
        map: map,
        anchorPoint: new google.maps.Point(0, -29)
    });

    autocomplete.addListener('place_changed', function () {
        infowindow.close();
        marker.setVisible(false);
        var place = autocomplete.getPlace();
        if (!place.geometry) {
            window.alert("Autocomplete's returned place contains no geometry");
            return;
        }

        // If the place has a geometry, then present it on a map.
        if (place.geometry.viewport) {
            map.fitBounds(place.geometry.viewport);
        } else {
            map.setCenter(place.geometry.location);
            map.setZoom(17);  // Why 17? Because it looks good.
        }
        marker.setIcon(/** @type {google.maps.Icon} */({
            url: place.icon,
            size: new google.maps.Size(71, 71),
            origin: new google.maps.Point(0, 0),
            anchor: new google.maps.Point(17, 34),
            scaledSize: new google.maps.Size(35, 35)
        }));
        marker.setPosition(place.geometry.location);
        var latitude = place.geometry.location.lat();
        var longitude = place.geometry.location.lng();
        marker.setVisible(true);
        console.log(latitude);
        console.log(longitude);
       
        var address = '';
        if (place.address_components) {
            address = [
              (place.address_components[0] && place.address_components[0].short_name || ''),
              (place.address_components[1] && place.address_components[1].short_name || ''),
              (place.address_components[2] && place.address_components[2].short_name || '')
            ].join(' ');
        }

        infowindow.setContent('<div><strong>' + place.name + '</strong><br>' + address);
        infowindow.open(map, marker);
    });

    // Sets a listener on a radio button to change the filter type on Places
    // Autocomplete.
    function setupClickListener(id, types) {
        var radioButton = document.getElementById(id);
        radioButton.addEventListener('click', function () {
            autocomplete.setTypes(types);
        });
    }

    setupClickListener('changetype-all', []);
    setupClickListener('changetype-address', ['address']);
    setupClickListener('changetype-establishment', ['establishment']);
    setupClickListener('changetype-geocode', ['geocode']);
}