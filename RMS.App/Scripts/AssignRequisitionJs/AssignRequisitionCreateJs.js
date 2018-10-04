
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
    //Vehicle Status
    $("#vehicleDD").change(function () {
        var selectedVehicleId = $("#vehicleDD").val();
        var data = { vehicleId : selectedVehicleId };
        $.ajax(
        {
            url: "/AssignRequisitions/GetVehicleStatusByVehicleId",
            data: data,
            type: "POST",
            success: function (response) {
                var status = response.split(',');
                $('#vehicleStatusdiv').modal();
                
                var statusInfo = "Requisition number: " + status[0] +"\n" +" Status: " + status[1] +"\n"+ " Driver Name: " + status[2];
                $("#vehicleDetails").html(statusInfo);
            },
            error: function (response) {
                alert("Error");
            }
        });

    });

    //Driver Status

    $("#searchableDD3").change(function () {
        var selectedDriverId = $("#searchableDD3").val();
        var data = { driverId: selectedDriverId };
        $.ajax(
        {
            url: "/AssignRequisitions/GetDriverStatusByDriverId",
            data: data,
            type: "POST",
            success: function (response) {
                var status = response.split(',');
                $('#driverStatusdiv').modal();
                var statusInfo = "Driver Name: " + status[0] + "\n" + " Requisition No: " + status[1] + "\n" + " Status: " + status[2];
                $("#details").html(statusInfo);
                //var status = response.split(',');

                //var statusInfo = "Driver Name: " + status[0] + "\n" + " Requisition No: " + status[1] + "\n" + " Status: " + status[2];
                //$("#driverStatusdiv").html(statusInfo);
            },
            error: function (response) {
                alert("Error");
            }
        });

    });

  
});



