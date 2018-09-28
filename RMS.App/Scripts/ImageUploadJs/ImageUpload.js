

$(document).ready(function() {

    $("#imageBrowse").change(function() {

        var File = this.files;
        if (File && File[0]) {
            ReadImage(File[0]);
        }
    });

});

var ReadImage= function(file) {
    var reader = new FileReader;
    var image = new Image;
    reader.readAsDataURL(file);
    reader.onload=function(_file) {
        image.src = _file.target.result;
        image.onload=function() {
            var height = this.height;
            var width = this.width;
            var type = file.type;
            var size = (file.size / 1024);

                if (height <= 300 && width <= 300 && size <= 100 && type == "image/jpeg" || type == "image/jpg" || type == "image/png") {

                    $("#targetImg").attr('src', _file.target.result);
                    $("#imgDescription").text(size +" KB " +"  (" + height + " X " + " " + width + " px) " + " " + type);
                    $("#imgPreview").show();

                } else {
                    //alert("Your image not as required!");
                    $("#dialog").dialog("open");
                }

           
            
        }
    }
}

var ClearPreview=function() {
    $("#imageBrowse").val('');
    $("#imgDescription").val('');
    $("#imgPreview").hide();
}

var UploadImage = function () {

    var file = $("#imageBrowse").get(0).files;
    var data = new FormData;
    data.append("ImageFile", file[0]);

    $.ajax({
        type: "POST",
        url: "/EmployeeImages/UploadImage",
        data: data,
        contentType: false,
        processData: false,
        success: function (imgId) {

            ClearPreview();

            $("#uploadedImage").append('<img src="/EmployeeImages/RetriveImage?imgId=' + imgId + '" class="img-responsive img-thumbnail" />');//style="width: 250px; height: 150px;"

            if (imgId>0) {
                $("#successMsg").text("Image uploaded successfully");
                $("#empImageId").val(imgId);
            } else {
                alert("Image uploaded failed!");
            }
            // $("#uploadedImage").append('<img src="/EmployeeProfileImages/' + response + '" class="img-responsive img-thumbnail" style="width: 180px; height: 180px;"/>');
        }
        
    });

}

$("#dialog").dialog({

    draggable: false,
    resizable: false,
    closeOnEscape: false,
    model: true,
    autoOpen:false
})