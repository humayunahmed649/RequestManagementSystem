
//AJAX Data Colling...........
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



