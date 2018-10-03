

$(document).ready(function () {

    var events = [];

    $.ajax({
        type: "GET",
        url: "/Schedule/GetAssignRequisition",
        success: function (data) {
            $.each(data, function (key, value) {
                events.push({
                    title: value.requisitionNumber,
                    employee: value.employeeName,
                    description: value.description,
                    start: moment(value.startTime),
                    end: value.endTime != null ? moment(value.endTime) : null,
                    driver: value.driver,
                    vehicle: value.vehicle,
                    color: "purpel",
                    textColor: "white"

                });
            });
            GenerateCalendar(events);
        },
        error: function (error) {
            alert("Failed");
        }

    });
});
function GenerateCalendar(events) {
    $('#calendar').fullCalendar("destroy");
    $('#calendar').fullCalendar({
        contentHeight: 400,
        defaultDate: new Date(),
        timeFormat: 'h(:mm)a',
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,basicWeek,basicDay,agenda'
        },

        eventLimit: true,
        eventColor: "green",
        allDay: false,

        events: events,
        eventClick: function (calEvent, jsEvent, view) {
            $('#myModal #requisitionNumber').text(calEvent.title);
            var $description = $('<div/>');

            $description.append($('<p>').html('<b>Employee Name : </b>' + calEvent.employee));
            $description.append($('<p>').html('<b>Driver Name : </b>' + calEvent.driver));
            $description.append($('<p>').html('<b>Description : </b>' + calEvent.description));
            $description.append($('<p>').html('<b>Vehicle Reg NO : </b>' + calEvent.vehicle));
            $description.append($('<p>').html('<b>Start Date Time : </b>' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
            if (calEvent.end != null) {
                $description.append($('<p>').html('<b>EndDateTime : </b>' + calEvent.end.format("DD-MMM-YYYY HH:mm a")));

            }

            $('#myModal #description').empty().html($description);
            $('#myModal').modal();
        }
    });
}