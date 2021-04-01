
function GetStateByCountryId(stateId) {
    $.post("/User/GetStateByCountryId",
        {
            Id: $("#CountryId").val(),
            SelectedId: $("#StateId").val()
        },
        function (data, status) {
            //clear previous filled states
            $("#StateId").html("");

            //append the states from selected country id
            $.each(data, function (i, item) {
                $("#StateId").append(
                    $('<option></option>').val(item.value).html(item.text));
            });

            $("#StateId").val(stateId).trigger('change');
        });
}

//Only Number validation
function isOnlyNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 32 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function pasteOnlyNumber(evt) {
    if (event.type == "paste") {
        var clipboardData = event.clipboardData || window.clipboardData;
        var pastedData = clipboardData.getData('Text');
        if (isNaN(pastedData)) {
            event.preventDefault();

        } else {
            return;
        }
    }
}

//Only Number validation
function isOnlyAmount(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 32 && (charCode < 48 || charCode > 57) && charCode != 46) {
        return false;
    }
    return true;
}

function IsOnlyText(event) {
    //evt = (evt) ? evt : window.event;
    var keyCode = (event.which) ? event.which : event.keyCode;
    if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && keyCode != 32) {
        return false;
    }
    return true;

}

function CloseToast() {
    $(".toastify").hide();
}

$('select').on('change', function () {
    $("select").removeClass("input-validation-error")
    $(".field-validation-error").html('');
});

$('.input-validation-error').on('change', function () {
    $(".input-validation-error").removeClass("input-validation-error")
    $(".field-validation-error").html('');
});

$('input').on('input', function () {
    $('.input-form').removeClass('has-error');
    $(".pristine-error").empty();
});


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

//setTimeout(function () {
//    $('#theDiv').html('More replacement text goes here');
//}, 2500);




