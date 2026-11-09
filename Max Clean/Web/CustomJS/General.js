function ForgotPassword(eml) {

    if (eml != "") {
        var data = {
            'EmailID': eml,
        };
        var parameters = JSON.stringify(data);
        $.ajax({
            type: "POST",
            url: '/Profile/ForgotPassword',
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.StatusCode == 200){
                    if (response.ObjectResult == true) {
                        $("#ResFPText").text("Forgot Password?");
                        $("#FPText").html("Forgot Password?");
                        alert("Please check your emailID for obtaining your password.");
                    } else {
                        $("#ResFPText").text("Forgot Password?");
                        $("#FPText").text("Forgot Password?");
                        alert("This email is not regeistered with us.");
                    }
                } else {
                    $("#ResFPText").text("Forgot Password?");
                    $("#FPText").html("Forgot Password?");
                    alert("There was an error processing your request. Please try again later.");
                }

            },
            error: function (e) {
                console.log(JSON.stringify(e));
                $("#ResFPText").text("Forgot Password?");
                $("#FPText").html("Forgot Password?");
                alert("There was an error processing your request. Please try again later.");
            }
        });
    

    }
}


//Main
//NewCus.Address = User["RAddr"];
//NewCus.AltMobileNumber = User["RAltMobile"];
//NewCus.CustomerName = User["RName"];
//NewCus.EmailID = User["REmailID"];
//NewCus.Password = User["RPassword"];
//NewCus.MobileNumber = User["RMobile"];

//Responsive
//NewCus.Address = User["ResAddr"];
//NewCus.AltMobileNumber = User["ResAltMobile"];
//NewCus.CustomerName = User["ResName"];
//NewCus.EmailID = User["ResEmailID"];
//NewCus.Password = User["ResPassword1"];
//NewCus.MobileNumber = User["ResMobile"];



