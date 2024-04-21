$("#newPassword").focus();const params = new URLSearchParams(window.location.search);let _userId = params.get('UserId');/*var encryptedValue = _userId;
var decryptedUserId2 = decryptValue(encryptedValue);*/$(document).ready(function () {    $('#UpdatePassword').submit(updatePassword);    $("#UpdatePassword").validate({
        rules: {
            newPassword: {
                required: true,
                minlength: 6, // Minimum password length requirement
            },
            confirmPassword: {
                required: true,
                equalTo: "#newPassword", // Ensure that the value matches the "New Password" field
            },
        },
        messages: {
            newPassword: {
                required: "Please enter a new password",
                minlength: "Password must be at least 6 characters long",
            },
            confirmPassword: {
                required: "Please confirm your new password",
                equalTo: "Passwords do not match",
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
});function updatePassword(e) {
    e.preventDefault(); // Prevent the form from submitting normally

    var userId = _userId; // Get the user ID from the query string
    var newPassword = $('#newPassword').val(); // Get the new password from the input field
  /*  var userId = decryptedUserId2;*/
    // Perform an AJAX request
    $.ajax({
        type: 'POST',
        url: '/ForgottenAndReset/UpdatePasswordForpatient', // Replace with the correct URL
        data: { userId: userId, newPassword: newPassword }, // Pass the user ID and new password as data to the server
        success: function (response) {
            if (response === true) {
                alert('Password updated successfully');
                window.location.href = '/Admin/AdminPage';
            } else {
                var errorMessage = $('<div class="alert alert-danger mt-3">Failed To Update</div>');
                errorMessage.insertAfter('#UpdatePassword');

                // Hide the error message after 3 seconds
                setTimeout(function () {
                    errorMessage.fadeOut('slow');
                }, 3000);
            }
        },
        error: function () {
            // Handle any errors that occur during the AJAX request
            alert('An error occurred during the password update');
        }
    });
}


/*function decryptvalue(encryptedvalue) {
    var decryptedvalue = null;

    // make an ajax request to the decryptvalue action
    $.ajax({
        type: 'post',
        url: '/forgottenandreset/decryptnumber',
        data: { encryptedvalue: encryptedvalue },
        async: false, // make the ajax request synchronous
        success: function (response) {
            // use the decrypted value returned from the controller
            decryptedvalue = response;
        },
        error: function () {
            // handle any errors that occur during the ajax request
            alert('an error occurred while decrypting the value');
        }
    });

    return decryptedvalue;
}*/