var lat;
var lon;
var NearbyLoc = [];

function Getlatlon() {
    if (navigator.onLine) {
        if (navigator.geolocation) {
            navigator.geolocation.watchPosition(showPosition, showError);
        }
        function showPosition(position) {
            
            lat = position.coords.latitude;
            lon = position.coords.longitude;
            GetNearbyStores();
            
        }
        function showError(error) {

            //Testing Module Begin
            lat = 12.992512;
            lon = 77.5921664;
            GetNearbyStores();
            setTimeout(function () { OpenSuccessModel(); }, 3000);
            //Testing Module End. Just for testing purposes. Please remove this module once it goes onto production HTTPS server.

            switch (error.code) {
                case error.PERMISSION_DENIED:                 
                    break;
                case error.POSITION_UNAVAILABLE:
                    alert("Location information is unavailable.");
                    break;
                case error.TIMEOUT:
                    alert("The request to get user location timed out.");
                    break;
                case error.UNKNOWN_ERROR:
                    alert("An unknown error occurred.");
                    break;
            }
        }
    }
    else {
        alert("No Internet Connecction , You System is Offline");
        lat = 300; //(300 denotes noInternet)
        lon = 300; //(300 denotes noInternet)
    }
}

//This block has been commented for facilitating testing. uncomment it once HTTPS hosting is done. 
//Block start
$(document).ready(function () {
    $("#just_load_please").on("click", function (e) {
        e.preventDefault();
        Getlatlon(); //Fetch Geo Location and stores nearby
        $("#loadMe").modal({
            backdrop: "static", //remove ability to close modal with click
            keyboard: false, //remove option to close with keyboard
            show: true //Display loader!
        });
        setTimeout(function () {
            $("#loadMe").modal("hide");
            if (NearbyLoc != null && NearbyLoc.length > 0) {
                OpenSuccessModel();
            }

        }, 5000);
    });
});
 //Block End  
  
//    //Adding a delay so we can see the functionality of the loader while request processes
//    $("#load_me_baby").on("click", function (e) {
//        e.preventDefault();
//        $("#loadMe").modal({
//            backdrop: "static", //remove ability to close modal with click
//            keyboard: false, //remove option to close with keyboard
//            show: true //Display loader!
//        });
      
//    });
//});


$(document).ready(function () {
    $("#just_load_please1").on("click", function (e) {
        e.preventDefault();
        Getlatlon(); //Fetch Geo Location and stores nearby
        $("#loadMe").modal({
            backdrop: "static", //remove ability to close modal with click
            keyboard: false, //remove option to close with keyboard
            show: true //Display loader!
        });
        setTimeout(function () {
            $("#loadMe").modal("hide");
            if (NearbyLoc != null && NearbyLoc.length > 0) {
              //  OpenSuccessModel();
            }
        }, 5000);

        //Adding a delay so we can see the functionality of the loader while request processes
        $("#load_me_baby").on("click", function (e) {
            e.preventDefault();
            $("#loadMe").modal({
                backdrop: "static", //remove ability to close modal with click
                keyboard: false, //remove option to close with keyboard
                show: true //Display loader!
            });

        });
    });
});


    function TakeUserToOrderPage() {
        window.location.pathname = "/Cleaning/Index";
    }

    function OpenSuccessModel() {
        $("#loadMe").modal("hide");
        $("#loadMe1").modal({
            backdrop: "static", //remove ability to close modal with click
            keyboard: false, //remove option to close with keyboard
            show: true //Display loader!
        });
        setTimeout(function () {
            $("#loadMe").modal("hide");
        }, 5000);

       
        //Adding a delay so we can see the functionality of the loader while request processes
        $("#load_me_baby").on("click", function (e) {
            e.preventDefault();
            $("#loadMe").modal({
                backdrop: "static", //remove ability to close modal with click
                keyboard: false, //remove option to close with keyboard
                show: true //Display loader!
            });
        });
    }

    function GetNearbyStores() {
        $('#SuccessFailure').html("Please wait while we look for nearby stores..");
        if (lat != undefined) {
            var data = {
                'Latitude': lat,
                'Longitude': lon
            };
            var parameters = JSON.stringify(data);
            $.ajax({
                type: "POST",
                url: '/CustomerOrder/getNearbyPlaces',
                data: parameters,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.ObjectResult != undefined) {
                        if (response.ObjectResult.length == 0) {
                            $('#SuccessFailure').html("Sorry, We are not serving at your location. Thank you for your patience.");

                        } else {
                            $('#SuccessFailure').html("Thank you for your patience. Please wait while we take you to our products page.");
                            NearbyLoc = response.ObjectResult;
                            OpenSuccessModel();
                        }
                    } else {
                        $('#SuccessFailure').html("There was an error finidng nearby stores. Please try again later.");
                    }

                },
                error: function (e) {
                    console.log(JSON.stringify(e));

                }
            });

        }
 
    }


