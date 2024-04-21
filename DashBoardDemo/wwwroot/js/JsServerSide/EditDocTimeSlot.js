$("#DoctorName").focus();

var tmpIeditId = 0;

const params = new Proxy(new URLSearchParams(window.location.search), {
    get: (searchParams, prop) => searchParams.get(prop),
});

let _Id = params.Id;
let _DoctorId = params.DoctorId;

if (_Id == null) {
    _Id = 0;
}




$(document).ready(function () {


    GetDataValue(_Id, _DoctorId);
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
            var ValData = {
                Id: _Id,
                DoctorId: _DoctorId,
                DoctorName: $("#DoctorName").val(),
                FromTime: $("#FromTime").val(),
                ToTime: $("#ToTime").val()
            };

            $.ajax({
                url: "/SerVerSide/UpdateData",
                type: "POST",
                dataType: "json",
                data: ValData,
                success: function (response) {
                    if (response.success) {
                        alert("Data updated successfully");
                        window.location.href = "/SerVerSide/ServerSideDoctorsTimeSlot";
                    } else {
                        alert("Unsuccessful: " + response.message);
                    }
                },
                error: function () {
                    alert("An error occurred while updating data.");
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
    tmpIeditId = _Id, _DoctorId;
    $.ajax({
        async: true,
        url: "/ServerSide/EdiitDoctorTimeslot",
        type: "GET",
        dataType: 'json',
        data: { Id: _Id, doctorId: _DoctorId },
        success: function (res) {
            var data = res;
            if (data != null) {
                $("#DoctorName").val(data.doctorName);

                var fmdate = moment(data.fromTime).format('YYYY-MM-DD HH:mm'); // Updated format
                $("#FromTime").val(fmdate);

                var totm = moment(data.toTime).format('YYYY-MM-DD HH:mm'); // Updated format
                $("#ToTime").val(totm);
            } else {
                alert('Data not found');
            }
        },
        error: function () {
            alert('An error occurred while retrieving the data');
        }
    });
}


