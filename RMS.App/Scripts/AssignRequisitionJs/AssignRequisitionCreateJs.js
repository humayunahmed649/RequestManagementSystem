
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
            success: function (data) {
                if (data.VehicleId == 1) {
                    alert("This Vehicle Is Not Available");
                } else {
                    alert("This Vehicle Is Available");
                }
                //var result = {};
                //result.VehicleId = data.VehicleId;
                //result.BrandName = data.BrandName;
                //$('#labelForStatus').html();
            },
        });

    });
  
});



