
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
                if (options==null) {
                    alert("Error Message :" + error + "\n Have not found any vehicle with this type!");
                } else {
                    $("#vehicleDD").append(options);
                }
            },
            error: function (response) {
                var error = response;
                alert("Error Message :" +error + "\n Have not found any vehicle with this type!");

            }

        });
    });
});