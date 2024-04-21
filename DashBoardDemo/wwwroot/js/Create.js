$("#DoctorName").focus();
const params = new Proxy(new URLSearchParams(window.location.search), {
    get: (searchParams, prop) => searchParams.get(prop),
});
let _Id = params.Id;
if (_Id == null) {
    _Id = 0;
}

$(document).ready(function () {

    if (_Id > 0) {
        GetDataValue(_Id);
        
    }
 
       $("#RegistrationForm").validate({
        rules: {
            DoctorName: {
                required: true,
                maxlength: 30,
                minlength: 1,
                whitespaceValidation: true,
                noDigits: true
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

            Speciality: {
                required: true
            },
        },
        messages: {
            DoctorName: {
                required: "Name is required",
                maxlength: "Too long",
                minlength: "Too short",
                noDigits: "Digits are not allowed"
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
            Speciality: {
                required: "Choose a valid slotime"
            },
        },
        submitHandler: function (RegForm) {
            var ValData = {
                Id: _Id,
                DoctorName: $("#DoctorName").val(),
                Phone: $("#Phone").val(),
                Email: $("#Email").val(),
                Speciality: $("#Speciality").val(),

            }

            $.ajax({
                async: true,
                url: "/AdminDemo/Register",
                type: "POST",
                dataType: 'json',
                data: ValData,
                success: function (res) {
                    if (res) {
                        alert("Doctor Data insertion/Updation successful");
                        window.location.href = "/AdminDemo/DoctorMaster";
                        
                    }
                    else {
                        alert("unsuccessful");
                    }
                }

            });


        }
    })
});


function GetDataValue(_Id) {
    if (_Id > 0) {
        tmpIeditId = _Id;
        $.ajax({
            async: true,
            url: "/AdminDemo/GetPutData",
            type: "GET",
            dataType: 'json',
            data: { Id: _Id },
            success: function (res) {
                var data = res;
                if (data != null) {
                    
                    $("#DoctorName").val(data.doctorName);
                    $("#Phone").val(data.phone);
                    $("#Email").val(data.email);
                    $("#Speciality").val(data.speciality);
                   

                }
                else {
                    alert('data not found');
                }


            }

        })
    }
}

$.validator.addMethod("whitespaceValidation", function (value, element) {
    return this.optional(element) || /^[^\s].*[^\s]$/.test(value.trim());
}, "Whitespace at the start or end is not allowed");

$.validator.addMethod("noDigits", function (value, element) {
    return this.optional(element) || /^[^\d]+$/.test(value);
}, "Digits are not allowed");


//For Email
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
