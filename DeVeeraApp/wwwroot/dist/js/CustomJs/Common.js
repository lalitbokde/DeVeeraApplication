

function hideloaderfn() {
  
    $("#loader").hide();
}
setTimeout(function () {
    $("#loader").hide();
}, 100);


function showloaderfn() {
$('.ovalicon').css('display', 'block');
$('.oval').css('display', 'block');
$('.loader').removeAttr("hidden");
}
function ShowLoader(formid) {
  
    var isValid = $("#" + formid).valid(); 
   
    if (isValid) {
        showloaderfn();
    }
}
function ShowLoaderUser(formid) {
    var isValid = $("#" + formid).valid();
    
    if (isValid) {
        showloaderfn();      
    }
    window.onload();
}
function onImageSelectionSuccess(data) {
 
    $("#" + data.imageFieldId).val(data.selectedImage.id);
    $("#" + data.imageFieldUrl).attr("src", data.selectedImage.imageUrl);
    //$("#superlarge-modal-size-preview").modal('hide');
}

function onFailed() {

}


function OpenImageSelectionModal(ImageFieldId, ImageFieldUrl) {
   
    $("#ImageFieldId").val(ImageFieldId);
    $("#ImageFieldUrl").val(ImageFieldUrl);  
  
    //$("#superlarge-modal-size-preview").modal('show');
}

function InActiveAllImage() {
  
    $('.form-check-input').click(function () {
        $('.form-check-input').not(this).prop('checked', false);
    });  
}

function hideshowqoutetrans() {
   
    var val = $('#randomquotespanish').is(':checked');
    if (val == true) {
        $('#quotesspanish').addClass('hidden', true)
    } else {
        $('#quotesspanish').removeClass('hidden', true)
    }
}
function hideshowqoutetrans() {
    
    var val = $('#randomquotespanish').is(':checked');
    if (val == true) {
        $('#quotesspanish').addClass('hidden', true)
    } else {
        $('#quotesspanish').removeClass('hidden', true)
    }
}
function hideshowqoute() {
   
    var val = $('#randomquote').is(':checked'); 
    if (val == true) {
        $('#quotes').addClass('hidden', true)
    } else {
        $('#quotes').removeClass('hidden', true)
    }
}
function hideshowqouteregist() {
   
    var val = $('#randomquote').is(':checked');
    if (val == true) {
        $('#quotesregistraion').addClass('hidden', true)
    } else {
        $('#quotesregistraion').removeClass('hidden', true)
    }
}
function hideshowqouteSpanish() {
    debugger
    var val = $('#randomquoteSpanish').is(':checked');
    if (val == true) {
        $('#quotesSpanish').addClass('hidden', true)
    } else {
        $('#quotesSpanish').removeClass('hidden', true)
    }
}

function UploadProfilepic()
{

var filename = $("#single-file-upload1 .dz-filename").text();




$.ajax({
        url: "/User/UploadProfilePicAWS",
    datatype: "text",
    type: "POST",
    data: { json: ch },
    success: function (data) {
        alert(data.message);
    },
    error: function () {
        // $("#testarea").html("ERROR");
        alert("Error");
    }
});
}
