
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
                var options = "<option> Select Upazila </option>";
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
                var options = "<option> Select Upazila </option>";
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

//Create Address Table............
var slNo = 0;
var index = 0;

//Present Address Table...........
$("#PACheckBox").click(function () {
    var address = getPresentAddressForForm();
    var tableBody = $("#addressDetails");

    var slCell = "<td>" + address.slNo + "</td>";
    var indexCell = "<td style='display:none'><input type='hidden' name='Addresses.Index' value='" + index + "'/></td>";
    var addressType = "<td><input type='hidden' name='Addresses[" + index + "].AddressType' value='" + address.addressType + "'/>" + address.addressType + "</td>";
    var addressLine1 = "<td><input type='hidden' name='Addresses[" + index + "].AddressLine1' value='" + address.addressLine1 + "'/>" + address.addressLine1 + "</td>";
    var addressLine2 = "<td><input type='hidden' name='Addresses[" + index + "].AddressLine2' value='" + address.addressLine2 + "'/>" + address.addressLine2 + "</td>";
    var postOffice = "<td><input type='hidden' name='Addresses[" + index + "].PostOffice' value='" + address.postOffice + "'/>" + address.postOffice + "</td>";
    var postCode = "<td><input type='hidden' name='Addresses[" + index + "].PostCode' value='" + address.postCode + "'/>" + address.postCode + "</td>";
    var divisionDd = "<td><input type='hidden' name='Addresses[" + index + "].DivisionId' value='" + address.divisionDD + "'/>" + address.divisionDD + "</td>";
    var districtDd = "<td><input type='hidden' name='Addresses[" + index + "].DistrictId' value='" + address.districtDD + "'/>" + address.districtDD + "</td>";
    var upazilaDd = "<td><input type='hidden' name='Addresses[" + index + "].UpazilaId' value='" + address.upazilaDD + "'/>" + address.upazilaDD + "</td>";

    var addressId = "<td><input type='hidden' name='Addresses[" + index + "].Id' value='" + address.addressId + "'/>" + address.addressId + "</td>";
    var employeeId = "<td><input type='hidden' name='Addresses[" + index + "].EmployeeId' value='" + address.employeeId + "'/>" + address.employeeId + "</td>";

    var tr = "<tr>" + slCell + indexCell + addressType + addressLine1 + addressLine2 + postOffice + postCode + divisionDd + districtDd + upazilaDd +addressId+employeeId +"</tr>";

    tableBody.append(tr);
    ++index;
    $("#PACheckBox").hide();

});

function getPresentAddressForForm() {
    ++slNo;
    var sl = slNo;
    var addressType = "Present Address";
    var addressLine1 = $("#addressLine1").val();
    var addressLine2 = $("#addressLine2").val();
    var postOffice = $("#postOffice").val();
    var postCode = $("#postCode").val();
    var divisionDropDown = $("#divisionDD").val();
    var districtDropDown = $("#districtDD").val();
    var upazilaDropDown = $("#upazilaDD").val();

    var addressId = $("#addressId").val();
    var employeeId = $("#employeeId").val();

    return {
        slNo:sl, addressType: addressType, addressLine1: addressLine1, addressLine2: addressLine2, postOffice: postOffice, postCode: postCode,
        divisionDD: divisionDropDown, districtDD:districtDropDown, upazilaDD:upazilaDropDown, addressId:addressId, employeeId:employeeId

    }
}

//Permanent address same as Present address
$("#PACheckBox1").click(function () {
    var address = getSamePermanentAddressForForm();
    var tableBody = $("#addressDetails");

    var slCell = "<td>" + address.slNo + "</td>";
    var indexCell = "<td style='display:none'><input type='hidden' name='Addresses.Index' value='" + index + "'/></td>";
    var addressType = "<td><input type='hidden' name='Addresses[" + index + "].AddressType' value='" + address.addressType + "'/>" + address.addressType + "</td>";
    var addressLine1 = "<td><input type='hidden' name='Addresses[" + index + "].AddressLine1' value='" + address.addressLine1 + "'/>" + address.addressLine1 + "</td>";
    var addressLine2 = "<td><input type='hidden' name='Addresses[" + index + "].AddressLine2' value='" + address.addressLine2 + "'/>" + address.addressLine2 + "</td>";
    var postOffice = "<td><input type='hidden' name='Addresses[" + index + "].PostOffice' value='" + address.postOffice + "'/>" + address.postOffice + "</td>";
    var postCode = "<td><input type='hidden' name='Addresses[" + index + "].PostCode' value='" + address.postCode + "'/>" + address.postCode + "</td>";
    var divisionDd = "<td><input type='hidden' name='Addresses[" + index + "].DivisionId' value='" + address.divisionDD + "'/>" + address.divisionDD + "</td>";
    var districtDd = "<td><input type='hidden' name='Addresses[" + index + "].DistrictId' value='" + address.districtDD + "'/>" + address.districtDD + "</td>";
    var upazilaDd = "<td><input type='hidden' name='Addresses[" + index + "].UpazilaId' value='" + address.upazilaDD + "'/>" + address.upazilaDD + "</td>";


    var addressId = "<td><input type='hidden' name='Addresses[" + index + "].Id' value='" + address.addressId + "'/>" + address.addressId + "</td>";
    var employeeId = "<td><input type='hidden' name='Addresses[" + index + "].EmployeeId' value='" + address.employeeId + "'/>" + address.employeeId + "</td>";

    var tr = "<tr>" + slCell + indexCell + addressType + addressLine1 + addressLine2 + postOffice + postCode + divisionDd + districtDd + upazilaDd + addressId + employeeId + "</tr>";

    tableBody.append(tr);
    ++index;
    $("#PACheckBox1").hide();
    $("#addressLine1p").hide();
    $("#addressLine2p").hide();
    $("#postOfficep").hide();
    $("#postCodep").hide();
    $("#divisionDDp").hide();
    $("#districtDDp").hide();
    $("#upazilaDDp").hide();
    $("#PACheckBox2").hide();
});

function getSamePermanentAddressForForm() {
    ++slNo;
    var sl = slNo;
    var addressType = "Permanent Address";
    var addressLine1 = $("#addressLine1").val();
    var addressLine2 = $("#addressLine2").val();
    var postOffice = $("#postOffice").val();
    var postCode = $("#postCode").val();
    var divisionDropDown = $("#divisionDD").val();
    var districtDropDown = $("#districtDD").val();
    var upazilaDropDown = $("#upazilaDD").val();

    var addressId = $("#addressId").val();
    var employeeId = $("#employeeId").val();

    return {
        slNo: sl, addressType: addressType, addressLine1: addressLine1, addressLine2: addressLine2, postOffice: postOffice, postCode: postCode,
        divisionDD: divisionDropDown, districtDD: districtDropDown, upazilaDD: upazilaDropDown, addressId:addressId, employeeId:employeeId

    }
}

//Permanent address.......
$("#PACheckBox2").click(function () {
    var address = getPermanentAddressForForm();
    var tableBody = $("#addressDetails");

    var slCell = "<td>" + address.slNo + "</td>";
    var indexCell = "<td style='display:none'><input type='hidden' name='Addresses.Index' value='" + index + "'/></td>";
    var addressType = "<td><input type='hidden' name='Addresses[" + index + "].AddressType' value='" + address.addressType + "'/>" + address.addressType + "</td>";
    var addressLine1 = "<td><input type='hidden' name='Addresses[" + index + "].AddressLine1' value='" + address.addressLine1 + "'/>" + address.addressLine1 + "</td>";
    var addressLine2 = "<td><input type='hidden' name='Addresses[" + index + "].AddressLine2' value='" + address.addressLine2 + "'/>" + address.addressLine2 + "</td>";
    var postOffice = "<td><input type='hidden' name='Addresses[" + index + "].PostOffice' value='" + address.postOffice + "'/>" + address.postOffice + "</td>";
    var postCode = "<td><input type='hidden' name='Addresses[" + index + "].PostCode' value='" + address.postCode + "'/>" + address.postCode + "</td>";
    var divisionDd = "<td><input type='hidden' name='Addresses[" + index + "].DivisionId' value='" + address.divisionDD + "'/>" + address.divisionDD + "</td>";
    var districtDd = "<td><input type='hidden' name='Addresses[" + index + "].DistrictId' value='" + address.districtDD + "'/>" + address.districtDD + "</td>";
    var upazilaDd = "<td><input type='hidden' name='Addresses[" + index + "].UpazilaId' value='" + address.upazilaDD + "'/>" + address.upazilaDD + "</td>";


    var addressId = "<td><input type='hidden' name='Addresses[" + index + "].Id' value='" + address.addressId + "'/>" + address.addressId + "</td>";
    var employeeId = "<td><input type='hidden' name='Addresses[" + index + "].EmployeeId' value='" + address.employeeId + "'/>" + address.employeeId + "</td>";

    var tr = "<tr>" + slCell + indexCell + addressType + addressLine1 + addressLine2 + postOffice + postCode + divisionDd + districtDd + upazilaDd+addressId+employeeId + "</tr>";

    tableBody.append(tr);
    ++index;
    $("#PACheckBox1").hide();
    $("#PACheckBox2").hide();

});

function getPermanentAddressForForm() {
    ++slNo;
    var sl = slNo;
    var addressType = "Permanent Address";
    var addressLine1 = $("#addressLine1p").val();
    var addressLine2 = $("#addressLine2p").val();
    var postOffice = $("#postOfficep").val();
    var postCode = $("#postCodep").val();
    var divisionDropDown = $("#divisionDDp").val();
    var districtDropDown = $("#districtDDp").val();
    var upazilaDropDown = $("#upazilaDDp").val();

    var addressId = $("#addressIdp").val();
    var employeeId = $("#employeeIdp").val();

    return {
        slNo: sl, addressType: addressType, addressLine1: addressLine1, addressLine2: addressLine2, postOffice: postOffice, postCode: postCode,
        divisionDD: divisionDropDown, districtDD: districtDropDown, upazilaDD: upazilaDropDown, addressId:addressId, employeeId:employeeId

    }
}