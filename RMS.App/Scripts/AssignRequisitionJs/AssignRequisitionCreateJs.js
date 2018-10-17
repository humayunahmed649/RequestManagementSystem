
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

                var $vehicleDetails = $('<div/>');
                if (response.Id != 0) {
                    $('#vehicleStatusdiv').modal();


                    //$("#vehicleDetails").append($('<p>').html('<b>Status : </b>' + response.StatusType));
                    $vehicleDetails.append($('<span class="form-control text-danger font-weight-bold">').html('<span class="text-success">Status : </span>' + response.RequisitionStatus.StatusType));
                    $vehicleDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">Requisition Number : </span>' + response.RequisitionNumber));
                    //$vehicleDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">Start Date Time : </span>' + response.Requisition.StartDateTime.format("DD-MMM-YYYY HH:mm a")));
                    //$vehicleDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">End Date Time : </span>' + response.Requisition.EndDateTime.format("DD-MMM-YYYY HH:mm a")));
                    $vehicleDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">Requestor Name : </span>' + response.Requisition.Employee.FullName));
                    $vehicleDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">Contact No : </span>' + response.Requisition.Employee.ContactNo));
                    $vehicleDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">From Place : </span>' + response.Requisition.FromPlace));
                    $vehicleDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">Destination Place : </span>' + response.Requisition.DestinationPlace));
                    $vehicleDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">Description : </span>' + response.Requisition.Description));
                    $vehicleDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">Driver Name : </span>' + response.Employee.FullName));
                    $vehicleDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">Driver Contact No : </span>' + response.Employee.ContactNo));
                    $('#vehicleStatusdiv #vehicleDetails').empty().html($vehicleDetails);
                }
                if (response.Id == 0) {
                    $('#vehicleStatusdiv').modal();
                    $vehicleDetails.append($('<span class="form-control text-justify font-weight-bold">').html('<span class="text-success">This Vehicle Is Available </span>'));
                    
                    $("#vehicleDetails").empty().html($vehicleDetails);

                }
                
            },
            
            error: function (response) {
                alert("Error");
            }
        });

    });

    //Driver Status

    $("#driverDD").change(function () {

        var selectedDriverId = $("#driverDD").val();
        var data = { driverId: selectedDriverId };
        $.ajax(
        {
            url: "/AssignRequisitions/GetDriverStatusByDriverId",
            data: data,
            type: "POST",
            success: function (response) {

                var $driverDetails = $('<div/>');
                if (response.Id != 0) {
                    $('#driverStatusdiv').modal();


                    //$("#vehicleDetails").append($('<p>').html('<b>Status : </b>' + response.StatusType));
                    $driverDetails.append($('<span class="form-control text-danger font-weight-bold">').html('<span class="text-success">Status : </span>' + response.RequisitionStatus.StatusType));
                    $driverDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">Requisition Number : </span>' + response.RequisitionNumber));
                    //$driverDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">Start Date Time : </span>' + response.Requisition.StartDateTime));
                    //$driverDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">End Date Time : </span>' + response.Requisition.EndDateTime.dateTime));
                    $driverDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">Requestor Name : </span>' + response.Requisition.Employee.FullName));
                    $driverDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">Contact No : </span>' + response.Requisition.Employee.ContactNo));
                    $driverDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">From Place : </span>' + response.Requisition.FromPlace));
                    $driverDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">Destination Place : </span>' + response.Requisition.DestinationPlace));
                    $driverDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">Description : </span>' + response.Requisition.Description));
                    $driverDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">Driver Name : </span>' + response.Employee.FullName));
                    $driverDetails.append($('<span class="form-control text-primary bold">').html('<span class="text-success">Driver Contact No : </span>' + response.Employee.ContactNo));
                    $('#driverStatusdiv #driverDetails').empty().html($driverDetails);
                }
                if (response.Id == 0) {
                    $('#driverStatusdiv').modal();
                    $driverDetails.append($('<span class="form-control text-justify font-weight-bold">').html('<span class="text-success">This Driver Is Available </span>'));
                    
                    $("#driverDetails").empty().html($driverDetails);
                }

            },
            error: function (response) {
                alert("Error");
            }
        });

    });

  
});



