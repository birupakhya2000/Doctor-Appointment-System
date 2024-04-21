$("#UserName").focus();

$(document).ready(function () {
    $("#showPassword").hide();

    $("#submit").click(function (e) {
        e.preventDefault();
        if ($("#AdminForm").valid()) {
            var pl = {
                UserName: $("#UserName").val(),
                PassCode: $("#passcode").val()
            };
            $.ajax({
                async: true,
                url: "/PatientLogin/Login",
                type: "POST",
                dataType: "json",
                data: pl,
                success: function (response) {
                    // Handle the response
                    if (response) {
                        // Redirect the user based on their role
                        if (response.message == "Patient") {
                            window.location.href = '/PatientLogin/PatientDashBoard';
                        } else if (response.message == "Admin") {
                            window.location.href = '/AdminDemo/Dashboard';
                        } else {
                            // Handle other roles or show an error message
                            alert('Unknown user role.');
                        }
                    } else {
                        // Show an error message
                        alert('Invalid username or password.');
                    }
                },
                error: function () {
                    // Show an error message
                    alert('Invalid username or password.');
                }
            });
        }
    });

            
    $("#passcode").on("input", function () {
        var passcodeInput = $("#passcode");
        var inputType = passcodeInput.attr("type");
        var isPasswordVisible = inputType === "text";

        var showPasswordIcon = $("#showPassword");

        if (passcodeInput.val() !== "") {
            showPasswordIcon.show();
        } else {
            showPasswordIcon.hide();
        }

        showPasswordIcon.click(function () {
            isPasswordVisible = !isPasswordVisible;

            if (isPasswordVisible) {
                passcodeInput.attr("type", "text");
                showPasswordIcon.html('<i class="fas fa-eye"></i>'); // Change the eye icon to open eye
            } else {
                passcodeInput.attr("type", "password");
                showPasswordIcon.html('<i class="fas fa-eye-slash"></i>'); // Change the eye icon to closed eye
            }
        });
    });
});


