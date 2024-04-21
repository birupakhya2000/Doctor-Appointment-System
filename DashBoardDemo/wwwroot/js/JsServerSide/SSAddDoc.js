$("#DoctorName").focus();


const params = new URLSearchParams(window.location.search);
let _Id = params.get('Id');
let _DoctorId = params.get('DoctorId');

if (_Id == null) {
    _Id = 0;
}

$(document).ready(function () {


    /* GetDataValue(_Id, _DoctorId);*/


    getAllDoctorNames();

    $("#RegistrationForm").validate({
        rules: {

            DoctorName: {
                required: true
            },
            FromTime: {
                required: true,
                date: true
            },
            ToTime: {
                required: true,
                date: true,
                min: function () {
                    return $("#FromTime").val();
                }
            }
        },
        messages: {

            DoctorName: {
                required: "Please enter a doctor name."
            },
            FromTime: {
                required: "Please enter a from time.",
                date: "Please enter a valid date."
            },
            ToTime: {
                required: "Please enter a to time.",
                date: "Please enter a valid date.",
                min: "To time cannot be before from time."
            }
        },
        submitHandler: function (form) {
            var selectedDoctorId = $("#DoctorDropdown").val();
            $("#DoctorId").val(selectedDoctorId);

            $.ajax({
                url: "/ServerSide/SubmitData",
                type: "POST",
                dataType: "json",
                data: $(form).serialize(),
                success: function (response) {
                    if (response.success) {
                        alert("Data submitted successfully");
                        window.location.href = "/ServerSide/ServerSideDoctorsTimeSlot";
                    } else {
                        alert("Unsuccessful: " + response.message);
                    }
                },
                error: function () {
                    alert("An error occurred while submitting data.");
                }
            });
        }
    });
});


function getAllDoctorNames() {
    $.ajax({
        url: "/ServerSide/GetAllDoctorNames",
        type: "GET",
        dataType: "json",
        success: function (response) {
            if (response.success) {
                var doctorNames = response.doctorNames;
                var dropdown = $("#DoctorDropdown");

                dropdown.empty();

                $.each(doctorNames, function (index, doctor) {
                    dropdown.append($("<option></option>")
                        .attr("value", doctor.doctorId)
                        .text(doctor.doctorName));
                });
            } else {
                alert(response.message);
            }
        },
        error: function () {
            alert("An error occurred while fetching doctor names.");
        }
    });
}

function GetDataValue(_Id, _DoctorId) {
    /*if (_Id > 0 && _DoctorId > 0) {
        tmpIeditId = _Id, _DoctorId;*/
    $.ajax({
        async: true,
        url: "/ServerSide/EdiitDoctorTimeslot",
        type: "GET",
        dataType: 'json',
        data: { Id: _Id, DoctorId: _DoctorId },
        success: function (res) {
            var data = res;
            if (data != null) {

                $("#DoctorName").val(data.doctorName);
                var fmdate = moment(data.FromTime).format('YYYY-MM-DDTHH:mm:ss'); // Format the datetime string
                $("#FromTime").val(fmdate);
                var todate = moment(data.ToTime).format('YYYY-MM-DDTHH:mm:ss'); // Format the datetime string
                $("#ToTime").val(todate);



            }
            else {
                alert('data not found');
            }

        },
        error: function () {
            alert('An error occurred while retrieving the data');
        }
    });
}


