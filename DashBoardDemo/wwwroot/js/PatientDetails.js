$(document).ready(function () {
    GetPatients();
});

function GetPatients() {
    $.ajax({
        url: '/Doctor/GetPatientData',
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
                    {title:"Sl No",
                        data: null, class: "text-center",
                        render: function (data, type, row, meta) {
                            var serialNumber = meta.row + 1;
                            return serialNumber;
                        }
                    },
                    /* { data: 'id' },*/
                    { title: "Name",data: 'name', class: "text-center" },
                    {
                        title: "Dob",
                        data: 'dob',
                        className: 'text-center',
                        render: function (data) {
                            return moment(data).format('DD-MM-YYYY');
                        }
                    },
                    { title: "phone", data: 'phone', class: "text-center" },
                    { title: "Email", data: 'email', class: "text-center" },
                    
                ]
            });
        }
    });
}



//Style
var customStyles = document.createElement('style');
customStyles.innerHTML = `
    .dataTables_wrapper {
        background-color: #f8f9fa;
        padding: 20px;
        border: 1px solid #dee2e6;
        border-radius: 10px;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    }

    .dataTables_wrapper .dataTables_length select,
    .dataTables_wrapper .dataTables_filter input {
        border: 1px solid #dee2e6;
        padding: 6px 12px;
        border-radius: 4px;
    }

    .dataTables_wrapper .dataTables_paginate .paginate_button {
        padding: 5px;
        margin-left: 2px;
        border: 1px solid #dee2e6;
        border-radius: 4px;
        background-color: #fff;
    }
    
    / Additional styling /
    .dataTables_wrapper {
        font-family: Arial, sans-serif;
    }

    .dataTables_wrapper .dataTables_info {
        font-size: 14px;
        color: #000; / Changed color to black /
        font-weight: normal; / Removed font-weight property /
    }

    .dataTables_wrapper .dataTables_paginate .paginate_button {
        font-size: 14px;
        color: #333;
        background-color: #eaeaea;
    }
`;

// Append the style element to the document head
document.head.appendChild(customStyles);
