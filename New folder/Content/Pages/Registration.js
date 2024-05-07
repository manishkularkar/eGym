$(document).ready(function () {
    GetRegistration();
});


function restrictAlphabets(e) {
    var x = e.which || e.keycode;
    if ((x >= 48 && x <= 58))
        return true;
    else
        return false;
}
var SaveRegistration = function () {
   
    $formData = new FormData();
    var Photo = document.getElementById('File1');
    if (Photo.files.length > 0) {
        for (var i = 0; i < Photo.files.length; i++) {
            $formData.append('File-' + i, Photo.files[i]);
        }

    }

    var reg_Id = $("#hdnReg_Id").val();
    var full_Name = $("#txtFull_Name").val();
    var mobile = $("#txtMobile").val();
    var plan = $("#txtPlan").val();
    var startDate = $("#txtStartDate").val();
    var dueDate = $("#txtDueDate").val();
    var address = $("#txtAddress").val();

    debugger;
   
    $formData.append('Reg_Id', reg_Id);
    $formData.append('Full_Name', full_Name);
    $formData.append('Mobile', mobile);
    $formData.append('Photo', Photo);
    $formData.append('Plans', plan);
    $formData.append('StartDate', startDate);
    $formData.append('DueDate', dueDate);
    $formData.append('Address', address);

    $.ajax({
        url: "/Registration/SaveRegistration",
        method: "Post",
        data: $formData,
        contentType: "application/json;charset=utf-8",
        contentType: false,
        processData: false,
        return: false,
        async: false,
        //success: function (response) {
        //   alert(response.Message);
        //},
        success: function (response) {
            alert(response.Message);
           
        }
    });
}

var GetRegistration = function () {
    $.ajax({
        url: "/Registration/GetRegistration",
        method: "Post",
        /* data:JSON.Stringify(model),*/
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
           
            var html = "";
            $("#tblReg tbody").empty();
            $.each(response.Message, function (Index, elementvalue) {
                html += "<tr><td>" + elementvalue.Full_Name +
                    "</td><td>" + elementvalue.Mobile +
                    "</td><td>" + elementvalue.Plans +
                    "</td><td>" + elementvalue.StartDate +
                    "</td><td>" + elementvalue.DueDate +
                    "</td><td>" + elementvalue.Address + "</td><td><button type='button' class='btn btn-success btn-sm' onclick='EditMember(" + elementvalue.Reg_Id + ")'><i class='bi bi-pencil-square' aria-hidden='true'></i></button>&nbsp<button type='button'class='btn btn-info btn-sm' onclick='GetMemberDetails(" + elementvalue.Reg_Id + ")'><i class='bi bi-eye-fill'></i></button>&nbsp<button type='button'class='btn btn-danger btn-sm' onclick='DeleteRegister(" + elementvalue.Reg_Id + ")'><i class='bi bi-trash-fill'></i></button></td></tr>";
            });
            $("#tblReg tbody").append(html);

        }
    });
}


var EditMember = function (Reg_Id) {
    debugger
    var model = {
        Reg_Id: Reg_Id
    };
    $.ajax({
        url: "/Registration/GetMemberDetails",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {

            $("#UpdateModal").modal('show');

            var statrdate = response.Message.StartDate.split("-");
            var sdate = statrdate[2] + "-" + statrdate[1] + "-" + statrdate[0];
            var duedate = response.Message.DueDate.split("-");
            var duedate = statrdate[2] + "-" + statrdate[1] + "-" + statrdate[0];


            $("#hdnReg_Id").val(response.Message.Reg_Id);
            $("#txtFull_Name").val(response.Message.Full_Name);
            $("#txtMobile").val(response.Message.Mobile);
          /*  $("#File1").html(response.model.Photo);*/
            $("#txtPlan").val(response.Message.Plans);
            $("#txtStartDate").val(sdate);
            $("#txtDueDate").val(duedate);
            $("#txtAddress").val(response.Message.Address);
        }
    });
}

var GetMemberDetails = function (Reg_Id) {

    var model = { Reg_Id: Reg_Id }
    $.ajax({
        url: "/Registration/GetMemberDetails",
        method: "post",
        type: "GET",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#SubscriptionModal").modal('show');

            $("#SubscriptionDetails").empty();
            var html = "";
            html += "<center>";
            html += "<div class='row'>";
            html += "<div class='col-sm-6'>";
            html += "<table>";
            html += "<tr>";
            html += "<td><img src='../Content/Pages/image/" + response.Message.Photo + "'height='200px' width='200px'/></td>"
            //html += "<td><label style='color:black'>Is Active :</label></td><td><span>" + response.model.IsActive + "</span></td>";
            html += "<tr>";
            html += "</table>"
            html += "</div>";
            html += "<div class='col-sm-6'>";
            html += "<table>"
            html += "<tr>";
            html += "<td><label style='color:black'>Member Name :</label></td><td><span>" + response.Message.Full_Name + "</span></td>";
            html += "<tr>";
            html += "<tr>";
            html += "<td><label style='color:black'>Mobile :</label></td><td><span>" + response.Message.Mobile + "</span></td>";
            html += "<tr>";
            html += "<tr>";
            html += "<td><label style='color:black'>Plan :</label></td><td><span>" + response.Message.Plans + "</span></td>";
            html += "<tr>";
            html += "<tr>";
            //html += "<td><label style='color:black'>Created Date :</label></td><td><span>" + response.model.CreatedDate + "</span></td>";
            html += "<tr>";
            html += "<tr>";
            html += "<td><label style='color:black'>Start Date :</label></td><td><span>" + response.Message.StartDate + "</span></td>";
            html += "<tr>";
            html += "<tr>";
            html += "<td><label style='color:black'>Due Date :</label></td><td><span>" + response.Message.DueDate + "</span></td>";
            html += "<tr>";
            html += "<td><label style='color:black'>Address :</label></td><td><span>" + response.Message.Address + "</span></td>";
            html += "<tr>";
            html += "</table>"
            html += "</div>";
            html += "</div>";
            html += "</center>";

            $("#SubscriptionDetails").append(html);
        }
    });
};

var DeleteRegister = function (Reg_Id) {
    debugger
    var model = { Reg_Id: Reg_Id };
    $.ajax({
        url: "/Registration/DeleteMember",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            alert(response.Message);
            GetRegistration();
        }
    });
}

//var TodayRegMember = function () {
//    const date = new Date();

//    let day = date.getDate();
//    //let month = date.getMonth() + 1;
//    //let year = date.getFullYear();
//    if (day == date.now()) {
       
//    }

//}
