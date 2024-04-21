//WORK -> TO UPDATE ISAPPROVED COLUMN VALUE IN THE DATABASE TABLE TIMESLOT_PATIENTS AND SHOW IN TABLE APPROVED OR REJECTED


$(document).ready(function () {
    loadData();
    $("#searchBtn").click(function () {
        FilterData();
    })
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
    var tableBody = $('#tblPatientTimeSlot tbody');
    tableBody.empty();

    $.each(data, function (index, item) {
        var patientName = item.name;
        var date = formatDate(item.date); // Convert date format
        var slotTime = item.slotTime;
        var isApproved = item.isApproved;
        var approvalStatus = '';
        var actionColumn = '';
        var approveButton = "";
        var rejectButton = "";
        if (isApproved != null && isApproved != undefined) {
            if (isApproved) {
                approvalStatus = 'Approved';
            } else {
                approvalStatus = 'Rejected';
            }
        }

        else {
            approveButton = '<button class="btn btn-success approve-btn" data-id="' + item.id + '">Approve</button>';
            rejectButton = '<button class="btn btn-danger reject-btn" data-id="' + item.id + '">Reject</button>';
        }
        actionColumn = '<td class="text-center approval-status">' + approvalStatus + ' ' + approveButton + ' ' + rejectButton + '</td>';

        var row = '<tr>' +
            '<td class="text-center">' + (index + 1) + '</td>' +
            '<td class="text-center">' + patientName + '</td>' +
            '<td class="text-center">' + date + '</td>' +
            '<td class="text-center">' + slotTime + '</td>' +
            actionColumn +
            '</tr>';

        tableBody.append(row);
    });
}


function formatDate(dateStr) {
    var dateObj = new Date(dateStr);
    var day = dateObj.getDate();
    var month = dateObj.getMonth() + 1;
    var year = dateObj.getFullYear();
    return day + '/' + month + '/' + year;
}

$(document).on('click', '.approve-btn', function () {
    var itemId = $(this).data('id');
    updateApprovalStatus(itemId, true);
});

$(document).on('click', '.reject-btn', function () {
    var itemId = $(this).data('id');
    updateApprovalStatus(itemId, false);
});

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




//For Search But not Implemented
function FilterData() {

    var PatientName = $("#PatientName").val();
    $.ajax({
        url: '/AdminDemo/SearchPatientsearch',
        type: 'POST',
        dataType: 'json',
        data: { PatientName: PatientName },
        success: function (data) {

            GetFilterData(data);
        },
        error: function () {
            alert('Error occurred while retrieving data.');
        }
    });

}

function GetFilterData(data) {
    var tableBody = $('#tblPatientTimeSlot tbody');
    tableBody.empty();


    $.each(data, function (index, item) {
        var row = '<tr>' +
            '<td class="text-center">' + item.id + '</td>' +
            '<td class="text-center">' + item.name + '</td>' +
            '<td class="text-center">' + item.date + '</td>' +
            '<td class="text-center">' + item.slotTime + '</td>' +
            item.actionColumn +
            '</tr>';

        tableBody.append(row);
    });
}

