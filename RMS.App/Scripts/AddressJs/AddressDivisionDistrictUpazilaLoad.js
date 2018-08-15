$(document).ready(function() {
    $("#divisionDD").change(function () {
        var selectedDivisionId = $("#divisionDD").val();
        var data = { divisionId: selectedDivisionId }
        $.ajax({
            url: "/Employees/GetDistrictByDivison",
            data: data,
            type: "POST",
            success:function(response) {
                $("#districtDD").empty();
                var options = "<option>Select</option>";
                $.each(response, function (key, value) {
                    options += "<option value'" + value.Id + "'>" + value.Name + "</option>";
                });
                $("#districtDD").append(options);
            },
            error:function() {
                alert("something went worng....!");
            }
            
        });
    });
    $(document).on('change', '#districtDD', function () {
            var districtId = $("#districtDD").val();
        });
});


