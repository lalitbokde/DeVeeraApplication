

function showloaderfn() {
    $(".ovalicon").removeClass("hidden", true);
    $("#loader").show();
}
function hideloaderfn() {
    $("#loader").hide();
}
setTimeout(function () {
    $("#loader").hide();
}, 100);


function LoadLoader() {
$('.ovalicon').css('display', 'block');
$('.oval').css('display', 'block');
$('.loader').removeAttr("hidden");
setTimeout(function () {
    $("#loader").hide();
}, 1000);
}
