


$(document).ready(function () {
    $("#btnSave").click(function () {
        debugger 
        var data = $("#departmentForm").serialize();
        $.ajax({
            type: "POST",
            url: "/Departments/Create",
            data: data,
            success: function (response) {
                alert("Data Has Been Saved Successfully");
            }
        })
    })
})