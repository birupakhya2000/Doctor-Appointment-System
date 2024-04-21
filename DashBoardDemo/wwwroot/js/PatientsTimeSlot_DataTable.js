$(document).ready(function () {
    loadData();
    
});

function loadData() {
    $.ajax({
        url: '/AdminDemo/GetTimeSlotPatients',
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
                data: null, class : "text-center",
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                },
               
                className: 'text-center'
            },
            { data: 'name', className: 'text-center' },

            { data: 'doctorName', className: 'text-center' },
           
            {
                data: 'date',
                className: 'text-center',
                render: function (data) {
                    return formatDate(data);
                }
            },
            { data: 'slotTime', className: 'text-center' },
            {
                data: 'isApproved', class: "text-center",
                orderable: false, 
                className: 'text-center',
                render: function (data, type, row) {
                    var approvalStatus = '';
                    var approveButton = '';
                    var rejectButton = '';

                    if (data !== null && data !== undefined) {
                        if (data) {
                            approvalStatus = 'Approved';
                        } else {
                            approvalStatus = 'Rejected';
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