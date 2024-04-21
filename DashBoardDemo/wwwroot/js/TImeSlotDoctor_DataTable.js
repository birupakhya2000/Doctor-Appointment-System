$(document).ready(function () {
    loadData();
});

function loadData() {
    $.ajax({
        url: '/AdminDemo/GetTimeSlotDoctor',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            renderTable(data);
        },
        error: function () {
            alert('Error occurred while retrieving data.');
        }
    });
}

function renderTable(data) {
    var table = $('#tblDoctorTimeSlot').DataTable({
        processing: true,
        lengthChange: true,
        lengthMenu: [[5, 10, 25, -1], [5, 10, 25, "All"]],
        searching: true,
        ordering: true,
        paging: true,
        data: data,
        columns: [
            {
                data: null, class:"text-center",
                render: function (data, type, row, meta) {
                   
                    var serialNumber = meta.row + 1;
                    return serialNumber;
                },
                orderable: false
            },
            { data: 'doctorName', class: "text-center" },
            { data: 'fromTime', render: formatDate, class: "text-center" },
            { data: 'toTime', render: formatDate, class: "text-center" },
            { data: null, orderable: false, render: formatTimeRange, class: "text-center" },
            {
                data: null, class: "text-center",
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<button onclick="EditData(' + data.id + ', \'' + data.doctorId + '\')" class="btn btn-primary"><i class="fas fa-edit"></i></button>';

                }
            }

        ]
    });

    // Update the index column after sorting or filtering
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}

function EditData(Id, DoctorId) {
    window.location.href = "/AdminDemo/EditViewDocTimeSlots?Id=" + Id + "&DoctorId=" + DoctorId;
}



function formatDate(data, type) {
    var dateTime = new Date(data);
    var formattedDate = dateTime.toLocaleDateString('en-US', { day: '2-digit', month: '2-digit', year: 'numeric' });
    var formattedTime = dateTime.toLocaleTimeString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true });
    return formattedDate + ' | ' + formattedTime.replace(':', ':');
}

function formatTimeRange(data, type) {
    var fromDateTime = new Date(data.fromTime);
    var toDateTime = new Date(data.toTime);

    var fromTime = fromDateTime.toLocaleTimeString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true });
    fromTime = fromTime.replace(':', ':');

    var toTime = toDateTime.toLocaleTimeString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true });
    toTime = toTime.replace(':', ':');

    return fromTime + ' - ' + toTime;
}


$(document).ready(function () {
    // DataTable initialization code

    // Add hover effect to table rows
    $('#tblDoctorTimeSlot tbody').on('mouseenter', 'tr', function () {
        $(this).css('background-color', '#F5F5F5');
    }).on('mouseleave', 'tr', function () {
        $(this).css('background-color', '');
    });
});