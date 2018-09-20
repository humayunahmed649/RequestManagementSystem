
//AJAX Data Colling...........
//Present Address..........
$(document).ready(function () {
    $("#divisionDD").
    change(function () {
        var selectedDivisionId = $("#divisionDD").val();
        var jsonData = { divisionId: selectedDivisionId };

        $.ajax({
            url: "/Employees/GetDistrictsByDivisionId",
            data: jsonData,
            type: "POST",
            success: function (response) {
                $("#districtDD").empty();
                var options = "<option> Select District </option>";
                $.each(response, function (key, districtObj) {
                    options += "<option value='" + districtObj.Id + "'>" + districtObj.Name + "</option>";
                });
                $("#districtDD").append(options);
            },
            error: function () {
                alert("Data not found!");
            }
        });
    });
    
});

$(document).ready(function () {
    $("#districtDD").
    change(function () {
        var selectedDistrictId = $("#districtDD").val();
        var jsonData = { districtId: selectedDistrictId };

        $.ajax({
            url: "/Employees/GetUpazilaByDistrictId",
            data: jsonData,
            type: "POST",
            success: function (response) {
                $("#upazilaDD").empty();
                var options = "<option> Select Upazila/Thana </option>";
                $.each(response, function (key, upazilaObj) {
                    options += "<option value='" + upazilaObj.Id + "'>" + upazilaObj.Name + "</option>";
                });
                $("#upazilaDD").append(options);
            },
            error: function () {
                alert("Data not found!");
            }
        });
    });
});

//AJAX Data Colling...........
//Permanent Address..........
$(document).ready(function () {
    $("#divisionDDp").
    change(function () {
        var selectedDivisionId = $("#divisionDDp").val();
        var jsonData = { divisionId: selectedDivisionId };

        $.ajax({
            url: "/Employees/GetDistrictsByDivisionId",
            data: jsonData,
            type: "POST",
            success: function (response) {
                $("#districtDDp").empty();
                var options = "<option> Select District </option>";
                $.each(response, function (key, districtObj) {
                    options += "<option value='" + districtObj.Id + "'>" + districtObj.Name + "</option>";
                });
                $("#districtDDp").append(options);
            },
            error: function () {
                alert("Data not found!");
            }
        });
    });
});

$(document).ready(function () {
    $("#districtDDp").
    change(function () {
        var selectedDistrictId = $("#districtDDp").val();
        var jsonData = { districtId: selectedDistrictId };

        $.ajax({
            url: "/Employees/GetUpazilaByDistrictId",
            data: jsonData,
            type: "POST",
            success: function (response) {
                $("#upazilaDDp").empty();
                var options = "<option> Select Upazila/Thana </option>";
                $.each(response, function (key, upazilaObj) {
                    options += "<option value='" + upazilaObj.Id + "'>" + upazilaObj.Name + "</option>";
                });
                $("#upazilaDDp").append(options);
            },
            error: function () {
                alert("Data not found!");
            }
        });
    });
});


//Permanent address same as Present address
$("#PACheckBox1").click(function () {

    var address = getSamePermanentAddressForForm();

    $("#addressTypep").val(address.addressType.toString());
    $("#addressLine1p").val(address.addressLine1.toString());
    $("#addressLine2p").val(address.addressLine2.toString());
    $("#postOfficep").val(address.postOffice.toString());
    $("#postCodep").val(address.postCode.toString());
    $("#divisionDDp").val(address.divisionDD);
    $("#districtDDp").val(address.districtDD);
    $("#upazilaDDp").val(address.upazilaDD);

    //readOnlyPermanentAddressAllField();
});

function getSamePermanentAddressForForm() {

    var addressType = "Permanent Address";
    var addressLine1 = $("#addressLine1").val();
    var addressLine2 = $("#addressLine2").val();
    var postOffice = $("#postOffice").val();
    var postCode = $("#postCode").val();
    var divisionDropDown = $("#divisionDD").val();
    var districtDropDown = $("#districtDD").val();
    var upazilaDropDown = $("#upazilaDD").val();

    return {
        addressType: addressType, addressLine1: addressLine1, addressLine2: addressLine2, postOffice: postOffice, postCode: postCode,
        divisionDD: divisionDropDown, districtDD: districtDropDown, upazilaDD: upazilaDropDown

    }
}

function readOnlyPermanentAddressAllField() {
    $('#addressLine1p').attr('disabled', true);
    $('#addressLine2p').attr('disabled', true);
    $('#postOfficep').attr('disabled', true);
    $('#postCodep').attr('disabled', true);
    $('#divisionDDp').attr('disabled', true);
    $('#districtDDp').attr('disabled', true);
    $('#upazilaDDp').attr('disabled', true);
}

