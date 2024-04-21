$(document).ready(function () {
    GetSetDataDoctors();
    GetDoctor();
   
});

function GetSetDataDoctors() {
    $.ajax({
        async: true,
        url: "/Doctor/GetDoctors",
        type: "GET",
        dataType: 'json',
        success: function (res) {
            var data = res;
            $("#tblEmployee tbody").html('');
            if (data != null && data.length > 0) {
                for (var i = 0; i < data.length; i++) {

                    var dt = '<tr><td>' + (i + 1) + '</td><td>' + data[i].doctorName + '</td><td>' + data[i].phone + '</td><td>' + data[i].email + '</td><td>' + data[i].speciality +  '</td></tr>';
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




function GetDoctor() {
    $.ajax({
        url: "/Doctor/GetDoctors",
        method: 'GET',
        success: function (res) {
            if (res) {
                var container = document.getElementById("containe");
                res.forEach(function (res) {
                    var card = document.createElement("div");
                    card.classList.add("card");

                    var name = document.createElement("h2");
                    name.textContent = res.doctorName;
                    card.appendChild(name);

                    var address = document.createElement("p");
                    address.textContent = res.speciality;
                    card.appendChild(address);

                    var button = document.createElement("button");
                    const btext = document.createTextNode("Book Appointment");
                    //var button = document.createElement("button");

                   /* button.addEventListener("click", function () {
                        var url = "/Patient/PatientView?doctorId=" + res.id + "&PatientId=" + res.id;
                       
                        window.location.href = url;
                    });*/
                    button.addEventListener("click", function () {
                        var url = "/Patient/PatientView?doctorId=" + res.id ;
                        window.location.href = url;
                    });


                    button.appendChild(btext);
                    card.appendChild(button);

                    container.appendChild(card);
                });
            } else {
                $('#announcement').append("<div> </div>");
            }
        }
    });
}

