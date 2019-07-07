$(document).ready(UpcomingAppointments);
$(document).on("click", "#upcomingappointments", UpcomingAppointments);
$(document).on("click", "#allappointments", AllAppointments);
function UpcomingAppointments() {
    $("#upcomingappointments").addClass("appointment");
    $("#allappointments").removeClass("appointment");
    $.ajax({
        url: "/Doctors/GetDoctorAppointments",
        data:
        {
            docId: $('#docid').text(),
            appointments: 0
        },
        type: "Get",
        success: function (data) {
            $("#appointmenttable").empty();
            let table = document.getElementById("appointmenttable");
            var row;
            var index;
            var name;
            var date
            var time;
            var status;
            var diagnosis;
            row = table.insertRow(0);
            row.className = "table-head"
            index = row.insertCell(0);
            name = row.insertCell(1);
            date = row.insertCell(2);
            time = row.insertCell(3);
            status = row.insertCell(4);
            index.innerHTML = "Appointment Token";
            name.innerHTML = "Patient Name";
            date.innerHTML = "Date";
            time.innerHTML = "Time Slot";
            status.innerHTML = "Status";
            for (let i = 0; i < data.length; i++) {
                row = table.insertRow((i + 1));
                row.className = "table-body"
                index = row.insertCell(0);
                name = row.insertCell(1);
                date = row.insertCell(2);
                time = row.insertCell(3);
                status = row.insertCell(4);
                diagnosis = row.insertCell(5);
                index.innerHTML = data[i].Id;
                name.innerHTML = data[i].PatientName;
                date.innerHTML = data[i].Date;
                time.innerHTML = data[i].TimeSlot;
                if (data[i].Status == "1") {
                    status.innerHTML = "<div class='btn btn-success btn-round'>Approved</div>";
                    diagnosis.innerHTML = "<button class='btn btn-default btn-square'><a href='/MedicalHistories/Create/" + data[i].PatientId + "?aid=" + data[i].Id + "'>Add Diagnosis</a></button>";
                }
            }
        }
    });
}
function AllAppointments() {
    $("#upcomingappointments").removeClass("appointment");
    $("#allappointments").addClass("appointment");
    $.ajax({
        url: "/Doctors/GetDoctorAppointments",
        data:
        {
            docId: $('#docid').text(),
            appointments: 1
        },
        type: "Get",
        success: function (data) {
            $("#appointmenttable").empty();
            let table = document.getElementById("appointmenttable");
            var row;
            var index;
            var name;
            var date
            var time;
            var status;
            var diagnosis;
            row = table.insertRow(0);
            row.className = "table-head"
            index = row.insertCell(0);
            name = row.insertCell(1);
            date = row.insertCell(2);
            time = row.insertCell(3);
            status = row.insertCell(4);
            index.innerHTML = "Appointment Token";
            name.innerHTML = "Patient Name";
            date.innerHTML = "Date";
            time.innerHTML = "Time Slot";
            status.innerHTML = "Status";
            for (let i = 0; i < data.length; i++) {
                row = table.insertRow((i + 1));
                row.className = "table-body"
                index = row.insertCell(0);
                name = row.insertCell(1);
                date = row.insertCell(2);
                time = row.insertCell(3);
                status = row.insertCell(4);
                diagnosis = row.insertCell(5);
                index.innerHTML = data[i].Id;
                name.innerHTML = data[i].PatientName;
                date.innerHTML = data[i].Date;
                time.innerHTML = data[i].TimeSlot;
                if (data[i].Status == "0") {
                    status.innerHTML = "<div class='btn btn-warning btn-round'>Pending</div>";
                    diagnosis.innerHTML = "<button class='btn btn-default btn-square'><a href='/Appointment/Edit/" + data[i].Id + "'>Edit</a></button>";
                } else if (data[i].Status == "1") {
                    status.innerHTML = "<div class='btn btn-success btn-round'>Approved</div>";
                    diagnosis.innerHTML = "<button class='btn btn-default btn-square'><a href='/MedicalHistories/Create/" + data[i].PatientId + "?aid=" + data[i].Id + "'>Add Diagnosis</a></button>";
                } else if (data[i].Status == "2") {
                    status.innerHTML = "<div class='btn btn-danger btn-round'>Cancelled</div>";
                } else if (data[i].Status == "3") {
                    status.innerHTML = "<div class='btn btn-info btn-round'>Closed</div>";
                    diagnosis.innerHTML = "<button class='btn btn-default btn-square'><a href='/Invoice/AddMedicines/" + data[i].Id + "'>Add Medicine</a></button>";
                }
            }
        }
    });
}
