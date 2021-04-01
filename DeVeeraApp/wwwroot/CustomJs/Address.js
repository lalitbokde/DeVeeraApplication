
//Sweet Alert for Delete
function ConfirmDialog(url) {
    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#0CC27E',
        cancelButtonColor: '#FF586B',
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
        confirmButtonClass: 'button w-24 mr-1 mb-2 bg-theme-9 text-white',
        cancelButtonClass: 'button w-24 mr-1 mb-2 bg-theme-6 text-white',
        buttonsStyling: false
    }).then(function () {
        var codeToExecute = url;
        var tmpFunc = new Function(codeToExecute);
        tmpFunc();
        showloaderfn();

    }, function (dismiss) {
        // dismiss can be 'overlay', 'cancel', 'close', 'esc', 'timer'
        if (dismiss === 'cancel') {
            swal(
                'Cancelled',
                'Your work request is safe :)',
                'error'
            )
        }
    })
}

//delete UserRole
function DeleteAddress(Id) {

    var jsonData = {

        id: Id


    };
    $.post("/Address/Delete",
        jsonData
        ,
        function (data, status) {
            if (data.success == true) {
                swal({
                    title: 'Success!',
                    text: "Address has been deleted.",
                    type: 'success',
                    confirmButtonColor: '#2f47c2',
                    confirmButtonText: 'Ok',
                    confirmButtonClass: 'button w-24 mr-1 mb-2 bg-theme-9 text-white',
                    buttonsStyling: false
                }).then(function () {
                    window.location.reload();
                })

            }
            else
                swal(
                    'Error!',
                    "Delete Failed",
                    'error',

                )
        });

}


//fill address details by selected address
function GetAddressByAddressId() {
    $.post("/Address/GetAddressByAddressId",
        {
            Id: $("#AddressId").val()
        },
        function (data, status) {

            $("#Address").val(data);
            
        });
}
