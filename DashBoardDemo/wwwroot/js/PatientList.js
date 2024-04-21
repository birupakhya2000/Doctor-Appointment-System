$(document).ready(function () {
    GetSetData();
    $("#searchBtn").click(function () {
        FilterData();
    })
    setCurrentDate();
});

function GetSetData() {
    $.ajax({
        async: true,
        url: "/AdminDemo/GetPatientList",
        type: "GET",
        dataType: 'json',
        success: function (res) {
            var data = res;
           
            $("#tblEmployee tbody").html('');
            if (data != null && data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    var dob = new Date(data[i].dob);
                    var formattedDob = dob.toLocaleDateString('en-US');
                    var dt = '<tr><td class="text-center">' + (i + 1) + '</td><td class="text-center">' + data[i].name + '</td><td class="text-center">' + formattedDob + '</td><td>' + data[i].phone + '</td><td class="text-center">' + data[i].email + '</td><td class="text-center"><button type = "button" style="background-color:#0000; border-radius:10px;" value= "Edit" onclick="EditData(' + data[i].id + ')"><i class="fa fa-pencil"></i></button><button style="background-color:#0000; border-radius:10px;margin-left:5px;" value= "Delete" onclick="Delete(' + data[i].id + ')"><i class="fa fa-trash"></i></button></td></tr>';
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
    window.location.href = "/AdminDemo/EditViewPatients?Id=" + _Id;
}


function Delete(_Id) {
    if (confirm("Are you want to delete data?")) {
        if (_Id > 0) {

            $.ajax({
                async: true,
                url: "/AdminDemo/DeleteData",
                type: "POST",
                dataType: 'json',
                data: { Id: _Id },
                success: function (res) {

                    if (res) {
                        alert('One row deleted Succesfully!');
                        window.location.href = "/AdminDemo/PatientList";
                        GetSetData();
                    }
                    else {
                        alert('Deletation is Unsuccesful!');
                    }
                }

            })
        }
    }
    else {

    }

}



/*function FilterData() {
    var fromDate = $("#fromDate").val();
    var toDate = $("#toDate").val();
    var PatientName = $("#PatientName").val();
    $.ajax({
        url: '/AdminDemo/Search',
        type: 'POST',
        dataType: 'json',
        data: { fromDate: fromDate, toDate: toDate, PatientName: PatientName },
        success: function (data) {

            GetFilterData(data);
        },
        error: function () {
            alert('Error occurred while retrieving data.');
        }
    });
    
}*/

function FilterData() {
    
    var PatientName = $("#PatientName").val();
    $.ajax({
        url: '/AdminDemo/Search',
        type: 'POST',
        dataType: 'json',
        data: {  PatientName: PatientName },
        success: function (data) {

            GetFilterData(data);
        },
        error: function () {
            alert('Error occurred while retrieving data.');
        }
    });

}


function setCurrentDate() {
    var now = new Date();
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = now.getFullYear() + "-" + (month) + "-" + (day);
    //$('#fromDate').val(today);
    $('#toDate').val(today);
}


//For searching Manually
function GetFilterData(data) {
    var tableBody = $('#tblEmployee tbody');
    tableBody.empty();


    $.each(data, function (index, item) {
        var row = '<tr>' +
            '<td>' + item.id + '</td>' +
            '<td>' + item.name + '</td>' +
            '<td>' + item.dob + '</td>' +
            '<td>' + item.phone + '</td>' +
            '<td>' + item.email + '</td>' +
            '<td>' + item.slotTime + '</td>' +
            '<td>' + item.action + '</td>' +
            '</tr>';

        tableBody.append(row);
    });
}


