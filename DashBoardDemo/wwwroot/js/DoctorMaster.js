$(document).ready(function () {
    GetSetDataDoctors();
  
});

function GetSetDataDoctors() {
    $.ajax({
        async: true,
        url: "/AdminDemo/GetDoctorMaster",
        type: "GET",
        dataType: 'json',
        success: function (res) {
            var data = res;
            $("#tblEmployee tbody").html('');
            if (data != null && data.length > 0) {
                for (var i = 0; i < data.length; i++) {

                    var dt = '<tr><td class="text-center">' + (i + 1) + '</td><td class="text-center">' + data[i].doctorName + '</td><td class="text-center">' + data[i].phone + '</td><td class="text-center">' + data[i].email + '</td><td class="text-center">' + data[i].speciality + '</td><td class="text-center"><button type = "button" style="background-color:#0000; border-radius:10px;" value= "Edit" onclick="EditData(' + data[i].id + ')"><i class="fas fa-edit"></i></button></td></tr>';
                    /*<td><button type = "button" style="background-color:green; border-radius:10px;" value= "Edit" onclick="EditData(' + data[i].id + ')"><i class="fa fa-pencil"></i></button><button style="background-color:red; border-radius:10px;margin-left:5px;" value= "Delete" onclick="Delete(' + data[i].id + ')"><i class="fa fa-trash"></i></button></td></tr>';*/
                    $("#tblEmployee tbody").append(dt);
                }
            }
            else {
                $("#tblEmployee tbody").html('');
            }
        }

    })
}

function EditData(_Id) {
    window.location.href = "/AdminDemo/Create?Id=" + _Id;
}



