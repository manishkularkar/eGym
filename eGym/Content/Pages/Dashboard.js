$(document).ready(function () {
    Renewal();
    GetCountMember();

});
var SubscriptionRenewal = function () {
 
    $.ajax({
        url: "/Registration/sendRenival",
        method: "Post",
        /* data:JSON.Stringify(model),*/
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            html = "";
            $("#tblSubscripation").empty();
            $.each(response.model, function (index, elementvalue) {
                
                html += "<div class='post-item clearfix'>";
                html += "<img src='../Content/Pages/image/" + elementvalue.Photo + "' alt=''>";
                html += "<h4><a>" + elementvalue.Full_Name + "</a></h4>";
                html += "<p><span class='badge bg-success'onclick='GetMemberDetails(" + elementvalue.Reg_Id + ")'>Details</span></p>";
                html += "</div>";
                html += "<hr style='height:2px; width:100%; border-width:0; color:black; background-color:black'>";
            });

            $("#tblSubscripation").append(html);

        }
    });
}


var Renewal = function () {
  
    $.ajax({
        url: "/Registration/Renival",
        method: "Post",
        /* data:JSON.Stringify(model),*/
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            html = "";
            $("#tblSubscripation").empty();
            $.each(response.model, function (index, elementvalue) {

                html += "<div class='post-item clearfix'>";
                html += "<img src='../Content/Pages/image/" + elementvalue.Photo + "' alt=''>";
                html += "<h4><a>" + elementvalue.Full_Name + "<br>" + elementvalue.DueDate + "</a></h4>";
                html += "<p><span class='badge bg-success'onclick='GetMemberDetails(" + elementvalue.Reg_Id + ")'>DETAILS</span>&nbsp;<span class='badge bg-danger'onclick='EditMember(" + elementvalue.Reg_Id + ")'>UPDATE</span></p>";
                html += "</div>";
                html += "<hr style='height:2px; width:100%; border-width:0; color:black; background-color:black'>";
            });
            $("#tblSubscripation").append(html);

        }
    });
}

var EditMember = function (Reg_Id) {
  
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


var GetCountMember = function () {
   
    $.ajax({
        url: "/Registration/GetCountMember",
        method: "post",
       // data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#TotalIncome").text(response.Message.TotalIncome);
            $("#TotalMember").text(response.Message.TotalMember);
            $("#TotalReniwal").text(response.Message.TotalReniwal);
            $("#TodayTotalMember").text(response.Message.TodayTotalMember);
            $("#YearTotalMember").text(response.Message.YearTotalMember);
            $("#MonthTotalMember").text(response.Message.MonthTotalMember);
            $("#TodayTotalReniwal").text(response.Message.TodayTotalReniwal);
            $("#MonthTotalReniwal").text(response.Message.MonthTotalReniwal);
            $("#YearTotalReniwal").text(response.Message.YearTotalReniwal);
            $("#TodayTotalIncome").text(response.Message.TodayTotalIncome);
            $("#MonthTotalIncome").text(response.Message.MonthTotalIncome);
            $("#YearTotalIncome").text(response.Message.YearTotalIncome);

            
        }
    });
}


