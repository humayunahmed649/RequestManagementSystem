
$(document).ready(function() {
    $("#vehicleTypeDD").change(function() {
        var selectedVehicleId = $("#vehicleTypeDD").val();
        var jsonData = { vehicleTypeId: selectedVehicleId };

        $.ajax(
        {
            url: "/Vehicles/GetByVehicleType",
            data: jsonData,
            type: "POST",
            success: function(response) {
                $("#vehicleDD").empty();
                var options = "<option >Select Vehicle</option>";
                $.each(response,
                    function(key, vehicle) {
                        options += "<option value='" + vehicle.Id + "'>" + vehicle.RegNo + "</option>";
                    });
                
            },
            error: function (response) {

            }

        });
    });
  
});

//$("#vehicleDD").change(function () {
//    var vehicleId = $("#vehicleDD").val();
//    var data = { status: vehicleId };
//    debugger;
//    $.ajax(
//    {
//        url: "/Vehicles/GetVehicleStatusByVehicleId",
//        data: data,
//        type: "POST",
//        success: function (response) {
//            alert("abc");
//        },
//        error: function (response) {

//        }
//    });
//    alert('kfjksdfaskdfsjakdfsa');

//});

