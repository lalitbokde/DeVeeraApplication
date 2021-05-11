

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

