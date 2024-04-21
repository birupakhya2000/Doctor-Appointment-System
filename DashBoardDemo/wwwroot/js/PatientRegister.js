$("#PatientName").focus();


$(document).ready(function () {
    var emailExists = false;
    var userExists = false;

    $('#Email').on('input', function () {
        var email = $(this).val();

        if (email) {
            $.ajax({
                url: "/PatientLogin/CheckDuplicacyEmailForPatients",
                type: 'POST',
                data: { email: email },
                success: function (response) {
                    if (response == true) {
                        emailExists = true;
                        $('#emailErrorMessage').show(); // Show the error message
                    } else {
                        emailExists = false;
                        $('#emailErrorMessage').hide(); // Hide the error message if it was previously shown
                    }
                },
                error: function () {
                    // Handle email existence check error
                }
            });
        } else {
            emailExists = false;
            $('#emailErrorMessage').hide(); // Hide the error message if the email field is empty
        }
    });

    $('#UserName').on('input', function () {
        var username = $(this).val();

        if (username) {
            $.ajax({
                url: "/PatientLogin/CheckDuplicacyUserNamePatients",
                type: 'POST',
                data: { username: username },
                success: function (response) {
                    if (response == true) {
                        userExists = true;
                        $('#usernameErrorMessage').show(); // Show the error message
                    } else {
                        userExists = false;
                        $('#usernameErrorMessage').hide(); // Hide the error message if it was previously shown
                    }
                },
                error: function () {
                    // Handle email existence check error
                }
            });
        } else {
            userExists = false;
            $('#usernameErrorMessage').hide(); // Hide the error message if the email field is empty
        }
    });

    $("#RegistrationForm").validate({
        rules: {
            Name: {
                required: true
            },
            UserName: {
                required: true
            },
            passcode: {
                required: true,
                minlength: 6 // Assuming a minimum password length of 6 characters
            },
            ConfirmPassword: {
                required: true,
                equalTo: "#passcode"
            },
            Dob: {
                required: true
            },
            Phone: {
                required: true,
                digits: true,
                minlength: 10,
                maxlength: 10
            },
            Email: {
                required: true,
                email: true,
                noDigitsAfterAt: true
            },
        },
        messages: {
            Name: {
                required: "<span class='error-message'>Please enter your name</span>"
            },
            UserName: {
                required: "<span class='error-message'>Please enter a username</span>"
            },
            passcode: {
                required: "<span class='error-message'>Please enter a password</span>"
            },
            ConfirmPassword: {
                required: "<span class='error-message'>Please confirm your password</span>",
                equalTo: "<span class='error-message'>Passwords do not match</span>"
            },
            Dob: {
                required: "<span class='error-message'>Please enter your date of birth</span>"
            },
            Phone: {
                required: "Please enter a valid 10-digit phone number.",
                digits: "Please enter only digits for the phone number.",
                minlength: "Phone number must be exactly 10 digits long.",
                maxlength: "Phone number must be exactly 10 digits long."
            },
            Email: {
                required: "Please enter an email address",
                email: "Please enter a valid email address",
                noDigitsAfterAt: "Numbers are not allowed after the '@' symbol"
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
            error.addClass("text-danger font-weight-bold"); // Add red color to the error message
            error.appendTo(element.parent());
        }
    });

    $('#patientregd').click(function (e) {
        e.preventDefault();
        if (emailExists && userExists) {
            alert("Email already exists. Please enter a different email.");
            alert("Username already exists. Please enter a different username.");
            return; // Prevent form submission
        } else if (emailExists) {
            alert("Email already exists. Please enter a different email.");
            return; // Prevent form submission
        } else if (userExists) {
            alert("Username already exists. Please enter a different username.");
            return; // Prevent form submission
        }

        var formData = {
            Name: $("#PatientName").val(),
            Dob: $("#Dob").val(),
            Phone: $("#Phone").val(),
            Email: $("#Email").val()
        };

        var timeSlotData = {
            UserName: $("#UserName").val(),
            passcode: $("#passcode").val()
        };

        $.ajax({
            url: '/PatientLogin/SubmitForm',
            type: 'POST',
            dataType: 'json',
            data: {
                patientViewModel: formData,
                patientLoginViewModel: timeSlotData
            },
            success: function (response) {
                if (response) {
                    alert("Data insertion successful");
                    window.location.href = "/PatientLogin/LoginPage";
                }
            },
            error: function () {
                alert('Error occurred while inserting data.');
            }
        });
    });
});


//For Date of Birth validation (not to select the future date )
function validateDob() {
    var dobInput = document.getElementById("Dob");
    var selectedDate = new Date(dobInput.value);
    var currentDate = new Date();

    // Set the time to midnight for both dates
    selectedDate.setHours(0, 0, 0, 0);
    currentDate.setHours(0, 0, 0, 0);

    if (selectedDate > currentDate) {
        alert("Please select a valid past date (including today) for the Date of Birth.");
        dobInput.value = "";
    }
}

$.validator.addMethod("emailFormat", function (value, element) {
    return this.optional(element) || /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/.test(value);
}, "Please enter a valid email address.");

$.validator.addMethod("noDigitsAfterAt", function (value, element) {
    var parts = value.split("@");
    if (parts.length > 1 && /\d/.test(parts[1])) {
        return false;
    }
    return true;
}, "Numbers are not allowed after the '@' symbol.");

