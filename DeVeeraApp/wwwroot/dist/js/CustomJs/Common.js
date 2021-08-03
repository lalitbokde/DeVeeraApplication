

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

function hideshowqoute() {
    debugger
    var val = $('#randomquote').is(':checked'); 
    if (val == true) {
        $('#quotes').addClass('hidden', true)
    } else {
        $('#quotes').removeClass('hidden', true)
    }
}