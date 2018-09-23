
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
                $("#vehicleDD").append(options);
            },
            error: function (response) {
                alert("Error");
            }

        });
    });
    $("#vehicleDD").change(function () {
        var selectedVehicleId = $("#vehicleDD").val();
        var data = { vehicleId : selectedVehicleId };
        $.ajax(
        {
            url: "/Vehicles/GetVehicleStatusByVehicleId",
            data: data,
            type: "POST",
            success: function (response) {
                $.each(response,
                    function (key, vehicle) {
                        if (data.vehicleId == data.VehicleId) {
                            alert("This vehicle is Available");
                        }
                        else {
                            alert("This vehicle is not Available");
                        }
                        //$("#vehicleStatusDD").append("<option>Assigned</option>");
                    });
                //$("#vehicleStatusDD").append(options);
                
            },
            error: function (response) {
                alert("Error");
            }
        });

    });
  
});



