

$(document).ready(function() {
    $(".datePicker").datetimepicker({
        format: "MM/DD/YYYY",
        icons: {
        date: "fa fa-calendar"

    }
    });
    $(".timePicker").datetimepicker({
        format: "LT",
        icons: {
            time: "fa fa-clock-o",
            up: "fa fa-arrow-up",
            down: "fa fa-arrow-down"
        }
    });
});