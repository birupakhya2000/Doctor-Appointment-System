$("#otp").focus();

const params = new URLSearchParams(window.location.search);

let _userId = params.get('UserId');

$(document).ready(function () {
    verifyOTP();
    $("#OtpVerify").validate({
        rules: {
            otp: {
                required: true,
                minlength: 1, // Minimum password length requirement
            },
           
        },
        messages: {
            otp: {
                required: "Please enter a new password",
                minlength: "Password must be at least 6 characters long",
            },
            
        },
        errorClass: "error-message",
        errorElement: "div",
        errorPlacement: function (error, element) {
            error.appendTo(element.parent());
        },
        highlight: function (element) {
            $(element).addClass("error-input");
        },
        unhighlight: function (element) {
            $(element).removeClass("error-input");
        },
        // Custom styles for error messages
        errorClass: "error-message",
        errorPlacement: function (error, element) {
            error.addClass("text-danger"); // Add red color to the error message
            error.appendTo(element.parent());
        },
    });


});

function verifyOTP() {
    $('#OtpVerify').submit(function (e) {
        e.preventDefault(); // Prevent the form from submitting normally
        var userId = _userId;
        var otp = $('#otp').val(); // Get the OTP value from the input field

        // Perform an AJAX request
        $.ajax({
            type: 'POST',
            url: '/ForgottenAndReset/VerifyOTP', // Replace with the correct URL
            data: { otp: otp, userId: userId }, // Pass the OTP as data to the server
            success: function (response) {
                // Check the response
                if (response != null) {
                    window.location.href = '/ForgottenAndReset/UpdatePassword?UserId=' + response;
                } else {
                    // OTP is not valid
                    // Display an error message
                    var errorMessage = $('<div class="alert alert-danger mt-3">Invalid OTP</div>');
                    errorMessage.insertAfter('#OtpVerify');

                    // Hide the error message after 3 seconds
                    setTimeout(function () {
                        errorMessage.fadeOut('slow');
                    }, 3000);
                }
            },
            error: function () {
                // Handle any errors that occur during the AJAX request
                alert('An error occurred during the AJAX request');
            }
        });
    });
}



