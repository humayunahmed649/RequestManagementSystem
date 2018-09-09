
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
                var options = "<option >Select...</option>";
                $.each(response,
                    function(key, vehicle) {
                        options += "<option value='" + vehicle.Id + "'>" + vehicle.RegNo + "</option>"
                    });

                $("#vehicleDD").append(options);

            },
            error: function(response) {

            }

        });
    });
});