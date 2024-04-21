$(document).ready(function () {
    loadData();

});

function loadData() {
    $.ajax({
        url: '/Doctor/GetPatientTimeSlotData',
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
    /*  var userSessionJson = sessionStorage.getItem("UserSession");
      var userSession = JSON.parse(userSessionJson);
  
      var filteredData = data.filter(function (item) {
          return item.patientId === userSession.UserId; // Filter data based on the current user's ID
      });*/
    $('#tblPatientTimeSlot').DataTable().clear().destroy(); // Destroy previous DataTable (if any)

    $('#tblPatientTimeSlot').DataTable({
        processing: true,
        lengthChange: true,
        lengthMenu: [[5, 10, 25, -1], [5, 10, 25, 'All']],
        searching: true,
        ordering: true,
        paging: true,
        destroy: true,
        data: data,
        columns: [
            {
                title: "Sl No",
                data: null, class: "text-center",
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                },

                className: 'text-center'
            },
            { title: "Name", data: 'name', className: 'text-center' },
            { title: "DoctorName", data: 'doctorName', className: 'text-center' },
            {
                title: "Date",
                data: 'date',
                className: 'text-center',
                render: function (data) {
                    return formatDate(data);
                }
            },
            { title: "SlotTime", data: 'slotTime', className: 'text-center' },
            {
                title: "Status",
                data: 'isApproved', class: "text-center",
                orderable: false,
                className: 'text-center',
                render: function (data, type, row) {
                    var approvalStatus = '';
                    var approveButton = '';
                    var rejectButton = '';

                    if (data !== null && data !== undefined) {
                        if (row.userRole == 'Patient') {
                            if (data == true) {
                                
                                approvalStatus = 'Approved';

                            } else if (data == false){
                                approvalStatus = 'Rejected';
                            }
                            else{
                                approvalStatus = 'Pending';
                            }


                            //approvalStatus = 'Pending';
                        }
                        else {
                            if (data) {
                                approvalStatus = 'Approved';
                            } else {
                                approvalStatus = 'Rejected';
                            }
                        }

                    } else {
                        approveButton = '<button class="btn btn-success approve-btn" data-id="' + row.id + '">Approve</button>';
                        rejectButton = '<button class="btn btn-danger reject-btn" data-id="' + row.id + '">Reject</button>';
                    }

                    return approvalStatus + ' ' + approveButton + ' ' + rejectButton;
                }
            }

        ],
        order: [[0, 'asc']] // Sort by the first column in ascending order
    });
}


$(document).on('click', '.approve-btn', function () {
    var itemId = $(this).data('id');
    updateApprovalStatus(itemId, true);
});

$(document).on('click', '.reject-btn', function () {
    var itemId = $(this).data('id');
    updateApprovalStatus(itemId, false);
});


function formatDate(dateStr) {
    var dateObj = new Date(dateStr);
    var day = dateObj.getDate();
    var month = dateObj.getMonth() + 1;
    var year = dateObj.getFullYear();
    return day + '/' + month + '/' + year;
}

function updateApprovalStatus(itemId, isApproved) {
    $.ajax({
        url: '/AdminDemo/UpdateApprovalStatus',
        type: 'POST',
        data: { id: itemId, isApproved: isApproved },
        success: function () {
            var buttonContainer = $('[data-id="' + itemId + '"]');
            var approvalStatusCell = buttonContainer.closest('tr').find('.approval-status');
            if (isApproved) {
                approvalStatusCell.text('Approved').addClass('approved-status');
            } else {
                approvalStatusCell.text('Rejected').addClass('rejected-status');
            }

            // Disable the buttons
            var approveButton = buttonContainer.find('.approve-btn');
            var rejectButton = buttonContainer.find('.reject-btn');
            approveButton.prop('disabled', true);
            rejectButton.prop('disabled', true);

            alert(isApproved ? 'Approved' : 'Rejected'); // Display an alert message
        },
        error: function () {
            alert('Error occurred while updating the approval status.');
        }
    });
}

$(document).ready(function () {
    // DataTable initialization code

    // Add hover effect to table rows
    $('#tblPatientTimeSlot tbody').on('mouseenter', 'tr', function () {
        $(this).css('background-color', '#F5F5F5');
    }).on('mouseleave', 'tr', function () {
        $(this).css('background-color', '');
    });
});



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
