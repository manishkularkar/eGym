var SaveNoticeInfo = function () {
    debugger;

    var id = $("#HdnId").val();
    var title = $("#txtFull_Name").val();
    var description = $("#txtDescription").val();
    var createdate = $("#HdnCreatedate").val();
    var isActive = $("#HdnisActive").val();
   
    var model = {

        Id:id,
        Title: title,
        Description: description,
        Createdate: createdate,
        IsActive: isActive,
    };

    $.ajax({
        url: "/Notice/SaveNoticeInfo",
        method: "Post",
        data: JSON.stringify(model),
        contentType: "Application/json;charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response.Message);
        },
        error: function (response) {
            alert(response.Message);
        }
    });
}
