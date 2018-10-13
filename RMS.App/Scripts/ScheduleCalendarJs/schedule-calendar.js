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
            eventColor: "green",


            events: events,
            eventClick: function(calEvent, jsEvent, view) {
                selectedRequisition = calEvent;
                $('#myModal #requisitionNumber').text(calEvent.title);

                $("#status").append($('<p>').html('<b>Status : </b>' + calEvent.status));
                var $description = $('<div/>');

                $description.append($('<span class="form-control">').html('<b>Employee Name : </b>' + calEvent.employee));
                $description.append($('<span class="form-control">').html('<b>Designation : </b>' + calEvent.employeeDesignation));
                $description.append($('<span class="form-control">').html('<b>Requestor Email : </b>' + calEvent.employeeEmail));
                $description.append($('<span class="form-control">').html('<b>Contact No : </b>' + calEvent.employeeContact));
                $description.append($('<span class="form-control">').html('<b>Description : </b>' + calEvent.description));
                $description.append($('<span class="form-control">').html('<b>From Place : </b>' + calEvent.fromPlace));
                $description.append($('<span class="form-control">').html('<b>Destination Place : </b>' + calEvent.destinationPlace));
                $description.append($('<span class="form-control text-danger">').html('<b>Status : </b>' + calEvent.status));

                $description.append($('<span class="form-control">').html('<b>Start Date Time : </b>' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
                if (calEvent.end != null) {
                    $description.append($('<span class="form-control">').html('<b>EndDateTime : </b>' + calEvent.end.format("DD-MMM-YYYY HH:mm a")));
                }

                $('#myModal #description').empty().html($description);
                if (calEvent.status == "New") {
                    $("#btnAssign").show();
                    $("#btnCancel").show();
                    $("#btnHold").show();
                    $("#btnCheckIn").hide();
                    $("#btnCheckOut").hide();

                }
                if (calEvent.status == "Hold") {
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
                    $("#btnHold").hide();
                }
                if (calEvent.status == "OnExecute") {
                    $("#btnCheckOut").show();
                    $("#btnAssign").hide();
                    $("#btnCheckIn").hide();
                    $("#btnCancel").hide();
                    $("#btnHold").hide();
                }
                if (calEvent.status == "Completed") {
                    $("#btnCheckOut").hide();
                    $("#btnAssign").hide();
                    $("#btnCheckIn").hide();
                    $("#btnCancel").hide();
                    $("#btnHold").hide();

                }
                if (calEvent.status == "Cancelled") {
                    $("#btnCheckOut").hide();
                    $("#btnAssign").hide();
                    $("#btnCheckIn").hide();
                    $("#btnCancel").hide();
                    $("#btnHold").hide();

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
    $('#btnCheckIn').click(function () {
        if (selectedRequisition != null) {
            var url = "/GatePass/CheckIn/";
            window.location = url + selectedRequisition.reqId;
        }

    });
    $('#btnCheckOut').click(function () {
        if (selectedRequisition != null) {
            var url = "/GatePass/CheckOut/";
            window.location = url + selectedRequisition.reqId;
        }

    });

    $("#btnCancel").click(function() {
        if (selectedRequisition != null) {
            var url = "/CancelRequisition/Create?statusId=";
            window.location = url + selectedRequisition.reqId;
        }
    })

});




