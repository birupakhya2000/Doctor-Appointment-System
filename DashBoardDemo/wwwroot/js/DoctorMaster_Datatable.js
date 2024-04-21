$(document).ready(function () {
    GetDoctorList();
});

function GetDoctorList() {
    $.ajax({
        url: '/AdminDemo/GetDoctorList',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
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
                        data: null, class : "text-center",
                        render: function (data, type, row, meta) {
                            var serialNumber = meta.row + 1;
                            return serialNumber;
                        }
                    },
                    { data: 'doctorName', class: "text-center" },
                    { data: 'phone', class: "text-center" },
                    { data: 'email', class: "text-center" },
                    { data: 'speciality', class: "text-center" },
                    {
                        data: null, class :"text-center",
                        orderable: false, 
                        render: function (data, type, row, meta) {
                            return '<button type="button" onclick="EditData(' + row.id + ')" class="btn btn-primary" ><i class="fas fa-edit"></i></button>';
                        }
                    }
                ]
            });
        }
    });
}


function EditData(_Id) {
    window.location.href = "/AdminDemo/Create?Id=" + _Id;
    
}



$(document).ready(function () {
    // DataTable initialization code

    // Add hover effect to table rows
    $('#tblEmployee tbody').on('mouseenter', 'tr', function () {
        $(this).css('background-color', '#F5F5F5');
    }).on('mouseleave', 'tr', function () {
        $(this).css('background-color', '');
    });
});