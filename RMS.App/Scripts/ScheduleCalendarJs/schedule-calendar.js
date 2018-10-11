//var url = $("#RedirectTo").val();

$(document).ready(function () {
    var events = [];
    var selectedRequisition = null;
    FetchEventAndRenderCalendar();

    function FetchEventAndRenderCalendar() {
        events = [];
        $.ajax({
            type: "GET",
            url: "/Schedule/GetAllRequisition",
            success: function(data) {
                $.each(data, function(key, value) {
                    events.push({
                        requisitionStatusId: value.Id,
                        reqId: value.RequisitionId,
                        status: value.StatusType,
                        title: value.Requisition.RequisitionNumber,
                        employee: value.Requisition.Employee.FullName,
                        employeeDesignation: value.Requisition.Employee.Designation.Title,
                        employeeEmail: value.Requisition.Employee.Email,
                        employeeContact: value.Requisition.Employee.ContactNo,
                        destinationPlace: value.Requisition.DestinationPlace,
                        fromPlace: value.Requisition.FromPlace,
                        description: value.Requisition.Description,
                        start: value.Requisition.StartDateTime,
                        end: value.Requisition.EndDateTime,
                        endTime: value.Requisition.EndDateTime != null ? value.Requisition.EndDateTime : null,

                        color: "purpel",
                        textColor: "white"

                    });
                });
                GenerateCalendar(events);
            },
            error: function(error) {
                alert("Failed");
            }

        });
    }


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
            eventColor: '#378006',
            events: events,
            eventClick: function(calEvent, jsEvent, view) {
                selectedRequisition = calEvent;
                $('#myModal #requisitionNumber').text(calEvent.title);

                $("#status").append($('<p>').html('<b>Status : </b>' + calEvent.status));
                var $description = $('<div/>');

                $description.append($('<p>').html('<b>Employee Name : </b>' + calEvent.employee));
                $description.append($('<p>').html('<b>Designation : </b>' + calEvent.employeeDesignation));
                $description.append($('<p>').html('<b>Requestor Email : </b>' + calEvent.employeeEmail));
                $description.append($('<p>').html('<b>Contact No : </b>' + calEvent.employeeContact));
                $description.append($('<p>').html('<b>Description : </b>' + calEvent.description));
                $description.append($('<p>').html('<b>From Place : </b>' + calEvent.fromPlace));
                $description.append($('<p>').html('<b>Destination Place : </b>' + calEvent.destinationPlace));

                $description.append($('<p>').html('<b>Start Date Time : </b>' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
                if (calEvent.end != null) {
                    $description.append($('<p>').html('<b>EndDateTime : </b>' + calEvent.end.format("DD-MMM-YYYY HH:mm a")));
                }

                $('#myModal #description').empty().html($description);
                if (calEvent.status == "New") {
                    $("#btnAssign").show();
                    $("#btnCancel").show();
                    $("#btnCheckIn").hide();
                    $("#btnCheckOut").hide();

                }
                if (calEvent.status == "Assigned") {
                    $("#btnCheckIn").show();
                    $("#btnAssign").hide();
                    $("#btnCheckOut").hide();
                    $("#btnCancel").hide();
                }
                if (calEvent.status == "OnExecute") {
                    $("#myModal #btnCheckOut").show();
                    $("#btnAssign").hide();
                    $("#btnCheckIn").hide();
                    $("#btnCancel").hide();

                }
                if (calEvent.status == "Hold") {
                    $("#myModal #btnAssign").show();
                    $("#btnCheckOut").hide();
                    $("#btnCheckIn").hide();
                    $("#btnCancel").hide();

                }
                $('#myModal').modal();
            }

        });
    }

    $('#btnAssign').click(function() {
        if (selectedRequisition != null) {
            var url = "/AssignRequisitions/Create?requisitionId=";
            window.location = url + selectedRequisition.reqId;
        }
           
    });

});




