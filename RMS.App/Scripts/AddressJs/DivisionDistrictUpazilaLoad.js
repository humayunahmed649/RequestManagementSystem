
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
    $("#divisionDD1").
    change(function () {
        var selectedDivisionId = $("#divisionDD1").val();
        var jsonData = { divisionId: selectedDivisionId };

        $.ajax({
            url: "/Employees/GetDistrictsByDivisionId",
            data: jsonData,
            type: "POST",
            success: function (response) {
                $("#districtDD1").empty();
                var options = "<option> Select District </option>";
                $.each(response, function (key, districtObj) {
                    options += "<option value='" + districtObj.Id + "'>" + districtObj.Name + "</option>";
                });
                $("#districtDD1").append(options);
            },
            error: function () {
                alert("Data not found!");
            }
        });
    });
});

$(document).ready(function () {
    $("#districtDD1").
    change(function () {
        var selectedDistrictId = $("#districtDD1").val();
        var jsonData = { districtId: selectedDistrictId };

        $.ajax({
            url: "/Employees/GetUpazilaByDistrictId",
            data: jsonData,
            type: "POST",
            success: function (response) {
                $("#upazilaDD1").empty();
                var options = "<option> Select Upazila </option>";
                $.each(response, function (key, upazilaObj) {
                    options += "<option value='" + upazilaObj.Id + "'>" + upazilaObj.Name + "</option>";
                });
                $("#upazilaDD1").append(options);
            },
            error: function () {
                alert("Data not found!");
            }
        });
    });
});