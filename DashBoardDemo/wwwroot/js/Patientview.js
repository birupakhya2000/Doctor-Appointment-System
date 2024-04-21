

$("#Name").focus();

/*var urlParams = new URLSearchParams(window.location.search);
var doctorId = urlParams.get('doctorId');*/

const params = new Proxy(new URLSearchParams(window.location.search), {
    get: (searchParams, prop) => searchParams.get(prop),
});

let _Id = params.Id;
let doctorId = params.doctorId;
if (_Id == null) {
    _Id = 0;
}


$(document).ready(function () {
    $("#spnSlotTimeDropdown").text("");
    if (_Id > 0) {
        GetDataValue(_Id);
        getDoctorDetails(_Id);
        getAllDoctorSlots();
        GetDataValuePatient(_Id);
    }

    setCurrentDate();
    validateDate();
    $("#RegistrationForm").validate({
        rules: {
            Name: {
                required: true,
                maxlength: 30,
                minlength: 1,
                whitespaceValidation: true,
                noDigits: true
            },
            DoctorName: {
                required: true
            },
            Dob: {
                required: true,
                date: true
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
            date: {
                required: true,
                date: true
            },
            SlotTime: {
                required: true
            },
        },
        messages: {
            Name: {
                required: "Name is required",
                maxlength: "Too long",
                minlength: "Too short",
                noDigits: "Digits are not allowed"
            },
            DoctorName: {
                required: "Doctor Name is required"
            },
            Dob: {
                required: 'Please enter your date of birth.',
                date: 'Please enter a valid date of birth.'
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
            date: {
                required: "Enter a valid date",
                date: "Please enter a valid date."
            },
            SlotTime: {
                required: "Choose a valid slotime"
            },
        },
    });

    $("#patientbooking").click(function (e) {
        /*e.preventDefault();*/
        $("#spnSlotTimeDropdown").text("");
        var patientData = {
            DoctorId: doctorId,
            Date: $("#date").val(),
            SlotTime: $("#SlotTimeDropdown").val()
        };
        if ($("#SlotTimeDropdown").val() != null) {
            $.ajax({
                type: 'POST',
                async: true,
                url: '/Patient/SubmitForm',
                data: {
                    timeSlot_Patients: patientData
                },
                dataType: 'json',
                success: function (response) {
                    // Handle success response
                    if (response) {
                        alert("Your Appoitnment Booking is Successful")
                        window.location.href = '/PatientLogin/PatientDashBoard';
                    }
                },
                error: function () {
                    // Handle error response
                    alert('An error occurred during the AJAX request');
                }
            });
        }
        else {

            $("#spnSlotTimeDropdown").text("Please select Slot time");
        }

        
    });
});

    

function setCurrentDate() {
    var now = new Date();
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = now.getFullYear() + "-" + (month) + "-" + (day);
    $('#date').val(today);

}

function validateDate() {
    var inputDate = document.getElementById("date").value;
    var currentDate = new Date().toISOString().split("T")[0];

    if (inputDate < currentDate) {
        alert("Please select today & future date.");
        document.getElementById("date").value = "";
        setCurrentDate();
    }
}

//FOR UPDATE PATIENTS DATA
function GetDataValue(_Id) {
    if (_Id > 0) {
        tmpIeditId = _Id;
        $.ajax({
            async: true,
            url: "/Patient/GetPutData",
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


//For insert DoctorName and patients Details in the  PatientsView form
$(document).ready(function () {
    var urlParams = new URLSearchParams(window.location.search);
    var doctorId = urlParams.get('doctorId');

    if (doctorId) {
        $.ajax({
            url: "/Doctor/GetPutData",
            type: "GET",
            data: { Id: doctorId },
            dataType: 'json',
            success: function (data) {
                if (data != null) {
                    $("#DoctorName").val(data.doctorName);
                    $("#SlotTime").val(data.slotTime);
                }
                else {
                    alert('Doctor data not found');
                }
            },
            error: function (xhr) {
                alert('Error fetching doctor data');
            }
        });
    }

    if (doctorId) {
        $.ajax({
            url: "/Patient/GetPutPatientData",
            type: "GET",
           
            dataType: 'json',
            success: function (data) {
                if (data != null) {
                    $("#PatientName").val(data.name);
                    var dob1 = moment(data.dob).format('YYYY-MM-DD');
                    $("#Dob").val(dob1);
                    $("#Phone").val(data.phone);
                    $("#Email").val(data.email);
                }
                else {
                    alert('Patient data not found');
                }
            },
            error: function (xhr) {
                alert('Error fetching patient data');
            }
        });
    }
});







/*INSERT SLOT TIME OF DOCTORS ACCORDING TO THEIR ID IN THE PATIENTVIEW FORM*/

$("#fetchSlotsBtn").click(function (event) {
    event.preventDefault(); // Prevent default form submission behavior

    var selectedDate = $("#date").val();

    if (!selectedDate) {
        alert('Please select a date.');
        return;
    }

    var urlParams = new URLSearchParams(window.location.search);
    var doctorId = urlParams.get('doctorId');

    if (!doctorId) {
        alert('Please select a doctor.');
        return;
    }

    $.ajax({
        url: '/Doctor/GetDoctorTimeSlots',
        type: 'GET',
        dataType: 'json',
        data: {
            selectedDate: selectedDate,
            doctorId: doctorId
        },
        success: function (response) {
            if (response.success) {
                var dropdown = $('#SlotTimeDropdown');
                dropdown.empty();

                if (response.timeSlots.length > 0) {
                    $.each(response.timeSlots, function (index, slot) {
                        dropdown.append($('<option></option>').text(slot).val(slot));
                    });
                } else {
                    dropdown.append($('<option disabled></option>').text('No slots available'));
                }
            } else {
                alert(response.message);
            }
        },
        error: function () {
            alert('Error occurred while fetching time slots.');
        }
    });
});



// Custom validation method to disallow only spaces or white spaces
$.validator.addMethod("whitespaceValidation", function (value, element) {
    return this.optional(element) || /^[^\s].*[^\s]$/.test(value.trim());
}, "Whitespace at the start or end is not allowed");

$.validator.addMethod("noDigits", function (value, element) {
    return this.optional(element) || /^[^\d]+$/.test(value);
}, "Digits are not allowed");








//For email;
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

