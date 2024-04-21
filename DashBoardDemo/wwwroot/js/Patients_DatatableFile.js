$(document).ready(function () {
    GetPatients();
});

function GetPatients() {
    $.ajax({
        url: '/AdminDemo/GetPatientsList',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            //console.log(response);
            $('#tblEmployee').DataTable({
                processing: true,
                lengthChange: true,
                lengthMenu: [[5, 10, 25, -1], [5, 10, 25, "All"]],
                searching: true,
                ordering: true,
                paging: true,
                data: response,
                columns: [
                    {
                        data: null, class: "text-center",
                        render: function (data, type, row, meta) {
                            var serialNumber = meta.row + 1;
                            return serialNumber;
                        }
                    },
                    /* { data: 'id' },*/
                    { data: 'name', class: "text-center" },
                    {
                        data: 'dob',
                        className: 'text-center',
                        render: function (data) {
                            return moment(data).format('DD-MM-YYYY');
                        }
                    },
                    { data: 'phone', class: "text-center" },
                    { data: 'email', class: "text-center"},
                    {
                        data: null, class : "text-center",
                        orderable: false, 
                        render: function (data, type, row, meta) {
                            return '<button onclick="EditData(' + row.id + ')" class="btn btn-primary"><i class="fas fa-edit"></i></button>'+
                                '<button onclick="Delete(' + row.id + ')" class="btn btn-danger" style="margin-left:5px;"><i class="fas fa-trash"></i></button>';
                        }
                    }
                ]
            });
        }
    });
}

function EditData(id) {
    window.location.href = "/AdminDemo/EditViewPatients?Id=" + id;
}

function Delete(id) {
    if (confirm("Are you sure you want to delete this data?")) {
        if (id > 0) {
            $.ajax({
                async: true,
                url: "/AdminDemo/DeleteData",
                type: "POST",
                dataType: 'json',
                data: { Id: id },
                success: function (res) {
                    if (res) {
                        alert('One row deleted successfully!');
                        window.location.href = "/AdminDemo/PatientList";
                        GetSetData();
                    } else {
                        alert('Deletion was unsuccessful!');
                    }
                }
            });
        }
    }
}


$(document).ready(function () {
    
    $('#tblEmployee tbody').on('mouseenter', 'tr', function () {
        $(this).css('background-color', '#F5F5F5');
    }).on('mouseleave', 'tr', function () {
        $(this).css('background-color', '');
    });
});