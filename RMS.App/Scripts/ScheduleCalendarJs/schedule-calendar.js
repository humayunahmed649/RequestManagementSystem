

var selectedEvent = null;
$(document).ready(function () {

    var events = [];
    FetchEventAndRenderCalendar();
    function FetchEventAndRenderCalendar() {
        events = [];
        $.ajax({
            type: "GET",
            url: "/Schedule/GetAssignRequisition",
            success: function (data) {
                $.each(data, function (key, value) {
                    events.push({
                        requisitionStatusId: value.statusId,
                        reqId: value.RequisitionId,
                        status: value.StatusType,
                        title: value.requisitionNumber,
                        employee: value.employeeName,
                        description: value.description,
                        start: moment(value.startTime),
                        end: moment(value.endTime),
                        endTime: value.endTime != null ? moment(value.endTime) : null,
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
    }

    $("#btnCheckIn").click(function () {
        //Open Modal for Check in
        //OpenCheckInForm();
        window.location = "http://localhost:1651/GatePass/CheckIn/" + selectedEvent.requisitionStatusId;

    });





    //$("#btnCheckedIn").click(function () {
    //    //call function for submit data to the server
    //    $.ajax({
    //        type: "POST",
    //        url: "/AssginRequisitions/CheckIn",

    //        success: function (data) {
    //            if (data.status) {
    //                //Refresh the calendar
    //                FetchEventAndRenderCalendar();
    //                $('#checkInModal').modal('hide');
    //            }

    //        },
    //        error: function () {
    //            alert("Failed");
    //        }


    //    });
    //});


    //function OpenCheckInForm() {
    //    if (selectedEvent != null) {
    //        $("#requisitionNumber").val(selectedEvent.title);
    //        $("#statusId").val(selectedEvent.requisitionStatusId);
    //        $("#txtRqNo").val(selectedEvent.title);
    //        $("#txtRqId").val(selectedEvent.reqId);
    //        $("#txtStatus").val(selectedEvent.status);
    //    }
    //    $("#myModal").modal("hide");
    //    $('#checkInModal').modal();
    //}



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
        eventAfterRender: function (event, $el, view) {
            var formattedTime = $.fullCalendar.formatDates(event.start, event.end, "HH:mm { - HH:mm}");
            // If FullCalendar has removed the title div, then add the title to the time div like FullCalendar would do
            if ($el.find(".fc-event-title").length === 0) {
                $el.find(".fc-event-time").text(formattedTime + " - " + event.title);
            }
            else {
                $el.find(".fc-event-time").text(formattedTime);
            }
        },
      

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
