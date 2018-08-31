
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

    var tr = "<tr>" + slCell + indexCell + addressType + addressLine1 + addressLine2 + postOffice + postCode + divisionDd + districtDd + upazilaDd +"</tr>";

    tableBody.append(tr);
    ++index;
    readOnlyPresentAddressAllField();
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


    return {
        slNo:sl, addressType: addressType, addressLine1: addressLine1, addressLine2: addressLine2, postOffice: postOffice, postCode: postCode,
        divisionDD: divisionDropDown, districtDD:districtDropDown, upazilaDD:upazilaDropDown
    }
}

function readOnlyPresentAddressAllField() {
    $("#PACheckBox").hide();
    $('#addressLine1').attr('disabled', true);
    $('#addressLine2').attr('disabled', true);
    $('#postOffice').attr('disabled', true);
    $('#postCode').attr('disabled', true);
    $('#divisionDD').attr('disabled', true);
    $('#districtDD').attr('disabled', true);
    $('#upazilaDD').attr('disabled', true);
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


    var tr = "<tr>" + slCell + indexCell + addressType + addressLine1 + addressLine2 + postOffice + postCode + divisionDd + districtDd + upazilaDd + "</tr>";

    tableBody.append(tr);
    ++index;
    readOnlyPermanentAddressAllField();
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

    return {
        slNo: sl, addressType: addressType, addressLine1: addressLine1, addressLine2: addressLine2, postOffice: postOffice, postCode: postCode,
        divisionDD: divisionDropDown, districtDD: districtDropDown, upazilaDD: upazilaDropDown

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


    var tr = "<tr>" + slCell + indexCell + addressType + addressLine1 + addressLine2 + postOffice + postCode + divisionDd + districtDd + upazilaDd+ "</tr>";

    tableBody.append(tr);
    ++index;
    readOnlyPermanentAddressAllField();
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

    return {
        slNo: sl, addressType: addressType, addressLine1: addressLine1, addressLine2: addressLine2, postOffice: postOffice, postCode: postCode,
        divisionDD: divisionDropDown, districtDD: districtDropDown, upazilaDD: upazilaDropDown

    }
}

function readOnlyPermanentAddressAllField() {
    $("#PACheckBox1").hide();
    $("#PACheckBox2").hide();
    $('#addressLine1p').attr('disabled', true);
    $('#addressLine2p').attr('disabled', true);
    $('#postOfficep').attr('disabled', true);
    $('#postCodep').attr('disabled', true);
    $('#divisionDDp').attr('disabled', true);
    $('#districtDDp').attr('disabled', true);
    $('#upazilaDDp').attr('disabled', true);
}