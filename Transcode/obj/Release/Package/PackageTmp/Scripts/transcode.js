$(document).ready(function () {
    
    // Page d'accueil   //

    // Gestion de forulaire de la page d'accueil
    var hm_conversionFormUrl = $("#homepage #conversionFormUrl");
    var hm_conversionFormFile = $("#homepage #conversionFormFile");
    var hm_conversionFormFormat = $("#homepage #conversionFormFormat");
    var hm_conversionFormAudioFormat = hm_conversionFormFormat.children("#format_audio");
    var hm_conversionFormVideoFormat = hm_conversionFormFormat.children("#format_video");
    var hm_conversionForm = $("#homepage #conversionForm");
    var audioFormats = [], videoFormats = [];

    $("#homepage #conversionFormFormat > #format_video > option").each(function (ind, elm) {
        audioFormats.push(elm.value);
    });

    $("#homepage #conversionFormFormat > #format_audio > option").each(function (ind, elm) {
        videoFormats.push(elm.value);
    });

    hm_conversionFormUrl.on('click, keydown, keypress, focusout', function () {
        if (hm_conversionFormUrl.val().length > 0)
            emptyInput(hm_conversionFormFile);
    });

    hm_conversionFormFile.on('click, keydown, keypress, focusout', function () {
        if (hm_conversionFormFile.val().length > 0)
            emptyInput(hm_conversionFormUrl);

        
    });

    hm_conversionFormFile.on('focusout', function () {
        if (hm_conversionFormFile.val().length > 0) {
            if (chosenUrlFileIsValid(hm_conversionFormFile.val())) {
                updateSelectList(hm_conversionFormFile.val());
            } else {
                emptyInput(hm_conversionFormFile);
            }
        }
        
    });

    hm_conversionFormUrl.on('focusout', function () {
        if (hm_conversionFormUrl.val().length > 0) {
            if (chosenUrlFileIsValid(hm_conversionFormUrl.val())) {
                updateSelectList(hm_conversionFormUrl.val());
            } else {
                emptyInput(hm_conversionFormUrl);
            }
        }
        
    });

    hm_conversionForm.submit(function (event) {
        //alert(hm_conversionFormFormat.val());
        //event.preventDefault();
    });

    function chosenUrlFileIsValid(fileUrl) {
        var extension = fileUrl.toString().split(".").pop();
        return $.inArray(extension, audioFormats.concat(videoFormats)) > -1;


        
    }

    function updateSelectList(fileUrl) {
        var extension = fileUrl.toString().split(".").pop();
        if ($.inArray(extension, videoFormats) > -1) {
            hm_conversionFormVideoFormat.hide();
            $("#homepage #conversionFormFormat > #format_video > option").prop('disabled', true);
        }
    }

    function emptyInput(inputObject) {
        inputObject.val("");
    }



    // Fin Page d'accueil //


});