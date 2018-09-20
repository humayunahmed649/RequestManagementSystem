$('#button1').on('click', function () {
    if ($("#issueform").valid()) {
        // do something here when the form is valid
        $('#myModal').modal('toggle');
    }
});