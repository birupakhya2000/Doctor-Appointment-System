$("#Name").focus();
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
            Name: {
                required: true,
                maxlength: 30,
                minlength: 1,
                whitespaceValidation: true,
                noDigits: true
            },
            
            Dob: {
                required: true
            },
            Phone: {
                required: true,
            },
            Email: {
                required: true,
                email: true
            },
           
        },
        messages: {
            Name: {
                required: "Name is required",
                maxlength: "Too long",
                minlength: "Too short",
                noDigits: "Digits are not allowed"
            },
            
            Dob: {
                required: "Enter a Valid Date of Birth"
            },

            Phone: {
                required: "Please enter a valid 10-digit phone number."
            },

            Email: {
                required: "Please enter an email address",
                email: "Please enter a valid email address"
            },
            
        },
        submitHandler: function (RegForm) {
            var ValData = {
                Id:_Id,
                Name: $("#Name").val(),
                Dob: $("#Dob").val(),
                Phone: $("#Phone").val(),
                Email: $("#Email").val()
            };

            $.ajax({
                async: true,
                url: "/AdminDemo/Submit",
                type: "POST",
                dataType: 'json',
                data: ValData,
                success: function (res) {
                    if (res) {
                        alert("Patients Data Updation successful");
                        window.location.href = "/AdminDemo/PatientList";

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
            url: "/AdminDemo/GetPutaPatientData",
            type: "GET",
            dataType: 'json',
            data: { Id: _Id },
            success: function (res) {
                var data = res;
                if (data != null) {

                    $("#Name").val(data.name);
                    var dob1 = moment(data.dob).format('YYYY-MM-DD');
                    $("#Dob").val(dob1);
                    $("#Phone").val(data.phone);
                    $("#Email").val(data.email);


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
