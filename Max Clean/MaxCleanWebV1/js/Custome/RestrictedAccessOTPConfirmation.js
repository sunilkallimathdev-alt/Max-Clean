
var AssignedOTP;




function VerifyOTP() {

    var data = {
        'otp': $('#USErOTP').val(),
        'mobile': $("#regmobilenumber").val()
    };

    var parameters = JSON.stringify(data);
    $.ajax({
        type: "POST",
        url: '/MaxClean/Verify_OTP',
        data: parameters,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var myobj = response;
            if (myobj.StatusCode == 200) {
                if (window.location.pathname.toLowerCase() != "/order/cart") {
                    if ($('#regmobilenumber').val()) {
                        PlaceRegistration();
                    }
                    $('#MessageOTP').html("Please wait while we register your profile...");
                } else {
                    var objcust = new Object();
                    objcust.CustomerName = null;
                    objcust.MobileNumber = $("#mobile").val();
                    objcust.Address = null;
                    objcust.EmailID = null;
                    objcust.Pincode = null;
                    localStorage.setItem("objcust", JSON.stringify(objcust));
                    window.location.href = '/Order/Address';
                }
            }
            else {
                alert("Incorrect OTP. Please try again.");
                $('#BtnOTP').prop("disabled", false);
            }
        },
        error: function (e) {
            console.log(JSON.stringify(e));
            alert("Something went wrong. Please try again later.");

        }
    });





    //if ($('#USErOTP').val() == AssignedOTP) {
    //    // $('#BtnOTP').prop("disabled", true);
    //    if (window.location.pathname.toLowerCase() != "/order/cart") {
    //        if ($('#regmobilenumber').val()) {
    //            PlaceRegistration();
    //        }
    //        $('#MessageOTP').html("Please wait while we register your profile...");
    //    } else {
    //        var objcust = new Object();
    //        objcust.CustomerName = null;
    //        objcust.MobileNumber = $("#mobile").val();
    //        objcust.Address = null;
    //        objcust.EmailID = null;
    //        objcust.Pincode = null;
    //        localStorage.setItem("objcust", JSON.stringify(objcust));
    //        window.location.href = '/Order/Address';
    //    }

    //} else {
    //    alert("Incorrect OTP. Please try again.");
    //    $('#BtnOTP').prop("disabled", false);
    //}
}

function PlaceRegistration() {

    var name = $("#regname").val();
    var mobile = $("#regmobilenumber").val();
    var email = $("#regemail").val();
    var pincode = $("#pincode").val();
    var password = $("#regpassword").val();
    var address = $("#regAddress").val();
    var altmobilenumber = $("#altmobilenumber").val();
    var myobj = new Object();

    myobj.CustomerName = $("#regname").val();
    myobj.EmailID = $("#regemail").val();
    myobj.MobileNumber = $("#regmobilenumber").val();
    myobj.Address = $("#regAddress").val();
    myobj.AltMobileNumber = $("#altmobilenumber").val();
    myobj.Password = $("#regpassword").val();
    myobj.Pincode = $("#w_pincode").val();
    myobj.IsActive = true;

    var ObjCustomer = JSON.stringify(myobj);

    $.ajax({
        type: "POST",
        url: '/MaxClean/RegisterCustomer',
        data: { ObjCustomer: JSON.stringify(myobj) },        
        // contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.StatusCode == 200) {
                alert('Successfully Registered');
                window.location.reload(true);
            }
            else if (response.StatusCode == 409) {
                alert('Mobile No./ Email ID already Registered,Please use another Mobile No./ Email ID');
            }
            else {
                alert('Registered Failed');
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



