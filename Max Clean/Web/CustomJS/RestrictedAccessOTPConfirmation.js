
var AssignedOTP;
function VerifysendOTP(Mob) {
              
                var data = {
                    'Mobile': parseInt(Mob)
                };
                var parameters = JSON.stringify(data);

                $.ajax({
                    type: "POST",
                    url: '/Home/confirmOTP',
                    data: parameters,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response != null) {
                            if (response != 1) {
                                if (response == 200) {
                                   // alert("Thank you, your mobile number is now verified.");
                                   
                                } else {
                                    console.log("Something went wrong while confirming OTP. Please try again later.");

                                }
                            } 
                        } else {
                           console.log("Error");
                        }
                    },
                    error: function (e) {
                        console.log(JSON.stringify(e));
                      //  alert("Something went wrong. Please try again later.");
                        return false;
                    }
                });

            
}

function sendOTPNow(Mobile) {
    $('#RAltMobile').prop("disabled", true);
    $('#RAddr').prop("disabled", true);
    $('#RName').prop("disabled", true);
    $('#REmailID').prop("disabled", true);
    $('#RMobile').prop("disabled", true);
    $('#RPassword').prop("disabled", true);
    $('#RegistrationMenu').click();

    //Open the OTP entry model
    $("#myModal").modal({
        backdrop: "static", //remove ability to close modal with click
        keyboard: false, //remove option to close with keyboard
        show: true //Display loader!
    });
     
        var data = {
            'Mobile': Mobile
        };

        var parameters = JSON.stringify(data);
    //disable sending message for dev purpose
        $.ajax({
            type: "POST",
            url: '/Home/SendOTP',
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.StatusCode == 200) {
                    if (response != null) {
                        if (response.ObjectResult > 1) {
                            //var OTP = prompt("Please enter OTP you just recieved on your mobile number.", "Enter OTP");
                            AssignedOTP = response.ObjectResult;
                            console.log("Successfully sent OTP to the number.");
                            //VerifysendOTP();
                            //alert("Thank you, your mobile number is now verified.");

                        } else if(response.ObjectResult == -1){
                            alert("This number already exists. Please try another number or login using the same.");
                            return false;
                        } 
                    } else {
                        alert("There was an error while sending an OTP. Please try again later.");
                    }
                }
              
            },
            error: function (e) {
                console.log(JSON.stringify(e));
                alert("Something went wrong. Please try again later.");
                return false;
            }
        });
    
}

function VerifyOTP() {
    if ($('#USErOTP').val() == AssignedOTP) {    
    //if ($('#USErOTP').val() == "1234") {
        $('#BtnOTP').prop("disabled",true);
       // VerifysendOTP(mobile);
        if ($('#ResMobile').val()) {
            PlaceResRegistration();
        } else {
            PlaceRegistration();
        }
       
        $('#MessageOTP').html("Please wait while we register your profile...");
       // $("#id01").modal("hide");

    } else {
        alert("Incorrect OTP. Please try again.");
        $('#BtnOTP').prop("disabled", false);
    }
}

function PlaceRegistration() {
   
    var parameters = $('#ForRegistration').serialize();

       // JSON.stringify(User);

    $.ajax({
        type: "POST",
        url: '/Home/RegisterUser',
        data: parameters,
       // contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == true) {
                $('#RAltMobile').prop("disabled", false);
                $('#RAddr').prop("disabled", false);
                $('#RName').prop("disabled", false);
                $('#REmailID').prop("disabled", false);
                $('#RMobile').prop("disabled", false);
                $('#RPassword').prop("disabled", false);
                $('#BtnOTP').prop("disabled", false);
                //var OTP = prompt("Please enter OTP you just recieved on your mobile number.", "Enter OTP");
                alert("Thank you for registering with us..!");
                window.location.pathname = "Home/Index";


            } else {
                alert("There was an error while Registering you. Please try again later.");
                $('#BtnOTP').prop("disabled", false);
                $('#RAltMobile').prop("disabled", false);
                $('#RAddr').prop("disabled", false);
                $('#RName').prop("disabled", false);
                $('#REmailID').prop("disabled", false);
                $('#RMobile').prop("disabled", false);
                $('#RPassword').prop("disabled", false);
            }

        },
        error: function (e) {
            console.log(JSON.stringify(e));
          //  alert("We are facing issue. Please try again later.");
            $('#BtnOTP').prop("disabled", false);
            $('#RAltMobile').prop("disabled", false);
            $('#RAddr').prop("disabled", false);
            $('#RName').prop("disabled", false);
            $('#REmailID').prop("disabled", false);
            $('#RMobile').prop("disabled", false);
            $('#RPassword').prop("disabled", false);
            return false;
        }
    });


}

function PlaceResRegistration() {

        var parameters = $('#ForResRegistrationForm').serialize();

        // JSON.stringify(User);

        $.ajax({
            type: "POST",
            url: '/Home/RegisterUser',
            data: parameters,
            // contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response == true) {
                    $('#ResAltMobile').prop("disabled", false);
                    $('#ResAddr').prop("disabled", false);
                    $('#ResName').prop("disabled", false);
                    $('#ResMobile').prop("disabled", false);
                    $('#ResEmailID1').prop("disabled", false);
                    $('#ResPassword1').prop("disabled", false);
                    $('#BtnOTP').prop("disabled", false);
                    //var OTP = prompt("Please enter OTP you just recieved on your mobile number.", "Enter OTP");
                    alert("Thank you for registering with us..!");
                    window.location.pathname = "Home/Index";


                } else {
                    //alert("There was an error while Registering you. Please try again later.");
                    $('#MessageOTP').html("There was an issue while sending OTP. Please try again later");
                    $("#id01").modal("hide");
                    $('#BtnOTP').prop("disabled", false);
                    $('#ResAltMobile').prop("disabled", false);
                    $('#ResAddr').prop("disabled", false);
                    $('#ResName').prop("disabled", false);
                    $('#ResMobile').prop("disabled", false);
                    $('#ResEmailID1').prop("disabled", false);
                    $('#ResPassword1').prop("disabled", false);
                }

            },
            error: function (e) {
                console.log(JSON.stringify(e));
                //  alert("We are facing issue. Please try again later.");
                $('#MessageOTP').html("There was an issue while sending OTP. Please try again later");
                $('#BtnOTP').prop("disabled", false);
                $('#ResAltMobile').prop("disabled", false);
                $('#ResAddr').prop("disabled", false);
                $('#ResName').prop("disabled", false);
                $('#ResMobile').prop("disabled", false);
                $('#ResEmailID1').prop("disabled", false);
                $('#ResPassword1').prop("disabled", false);
                return false;
            }
        });
    }

