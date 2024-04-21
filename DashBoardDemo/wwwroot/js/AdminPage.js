$("#UserName").focus();

$(document).ready(function () {
    $("#showPassword").hide();
    $("#submit").click(function () {
        $("#message").html("Logging in...");

        var pl = {
            UserName: $("#UserName").val(),
            Passcode: $("#passcode").val(),

        };
        $.ajax({
            async: true,
            url: "/Admin/Login",
            type: "POST",
            dataType: "json",
            data: pl,
            success: function (res) {
                if (res) {
                    window.location.href = "/AdminDemo/DashBoard";
                    alert("Login Successful!");
                    $("#AdminForm")[0].reset();
                } else {
                    // Check patient login
                    $.ajax({
                        async: true,
                        url: "/PatientLogin/Login",
                        type: "POST",
                        dataType: "json",
                        data: pl,
                        success: function (res) {
                            if (res) {
                                window.location.href = "/Doctor/DoctorList";
                                alert("Login Successful!");
                                $("#AdminForm")[0].reset();
                            } else {
                                alert("Login Failed!");
                                $("#AdminForm")[0].reset();
                            }
                        },
                        error: function () {
                            alert("An error occurred while processing the request");
                        }
                    });
                }
            },
            error: function () {
                alert("An error occurred while processing the request");
            }
        });
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


$("#UserName").focus();

$(document).ready(function () {
    $("#showPassword").hide();
    $("#submit").click(function () {
        $("#message").html("Logging in...");

        var pl = {
            UserName: $("#UserName").val(),
            Passcode: $("#passcode").val(),

        };
        $.ajax({
            async: true,
            url: "/Admin/Login",
            type: "POST",
            dataType: "json",
            data: pl,
            success: function (res) {
                if (res) {
                    window.location.href = "/AdminDemo/DashBoard";
                    alert("Login Successful!");
                    $("#AdminForm")[0].reset();
                } else {
                    // Check patient login
                    $.ajax({
                        async: true,
                        url: "/PatientLogin/Login",
                        type: "POST",
                        dataType: "json",
                        data: pl,
                        success: function (res) {
                            if (res) {
                                window.location.href = "/Doctor/DoctorList";
                                alert("Login Successful!");
                                $("#AdminForm")[0].reset();
                            } else {
                                alert("Login Failed!");
                                $("#AdminForm")[0].reset();
                            }
                        },
                        error: function () {
                            alert("An error occurred while processing the request");
                        }
                    });
                }
            },
            error: function () {
                alert("An error occurred while processing the request");
            }
        });
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


